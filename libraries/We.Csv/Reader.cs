using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text.RegularExpressions;
using We.Results;

namespace We.Csv;

public class ReaderColumnSetException : Exception
{
    public ReaderColumnSetException(string? message, Exception? innerException)
        : base(message, innerException) { }
}

public sealed record ReaderResponse<T>(int Index, T Value, Reader<T> Reader) where T : class, new();

public interface ICsvReader<T> where T : class, new()
{
    IObservable<ReaderResponse<T>> OnReadLine { get; }
    string Filename { get; set; }
    bool HasHeader { get; set; }
    char Separator { get; set; }
    int LineRead { get; }
    Task<Result> Start(CancellationToken cancellationToken = default);
}

public class Reader<T> : ICsvReader<T> where T : class, new()
{
    private readonly ISubject<ReaderResponse<T>> _onReadLine = new Subject<ReaderResponse<T>>();
    public IObservable<ReaderResponse<T>> OnReadLine => _onReadLine.AsObservable();
    public string Filename { get; set; } = string.Empty;
    public bool HasHeader { get; set; } = true;
    public char Separator { get; set; } = ';';
    public int LineRead { get; private set; } = 0;
    protected ILogger<Reader<T>>? Logger { get; }

    private Func<string?, char, T> _onReadLineMapping;
    private readonly List<MapColumn> columns = new List<MapColumn>();

    public Reader(string filename, bool hasHeader, char separator) : this(default)
    {
        this.Filename = filename;
        this.HasHeader = hasHeader;
        this.Separator = separator;
    }

    public Reader(ILogger<Reader<T>>? logger)
    {
        this.Logger = logger;
        _onReadLineMapping = OnReadLineMapping;

        MapColumns();
    }

    private void MapColumns()
    {
        Logger?.LogTrace("Begin mapping columns");
        var properties = typeof(T)
            .GetProperties()
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(CsvFieldAttribute)));
        foreach (var property in properties)
        {
            var attr = property.GetCustomAttribute<CsvFieldAttribute>();
            columns.Add(new MapColumn(attr?.Index ?? 0, attr?.Name ?? string.Empty, property));
        }
        Logger?.LogTrace("Mapping Columns ended");
    }

    protected virtual T OnReadLineMapping(string? line, char separator)
    {
        T result = new T();
        var values = line?.Split(separator) ?? new string[0];
        foreach (var col in columns)
        {
            if (col.Index >= 0 && col.Index <= values.Length)
            {
                try
                {
                    var v = values[col.Index];
                    var vv = To(
                        v,
                        col?.Property?.GetSetMethod()?.GetParameters()?.First().ParameterType
                    );
                    col?.Property?.GetSetMethod()?.Invoke(result, new object?[] { vv });
                }
                catch (Exception ex)
                {
                    throw new ReaderColumnSetException(
                        $"Column:{col.InternalName} -> {ex.Message}",
                        ex
                    );
                }
            }
        }

        return result;
    }

    protected object To(string? from, Type? to)
    {
        return to?.Name switch
        {
            nameof(DateOnly) => ToDateOnly(from),
            nameof(String) => from ?? string.Empty,
            nameof(Int32)
              => Int32.Parse(string.IsNullOrEmpty(from) ? "0" : (from?.Split(".")[0] ?? "0")),
            nameof(Double)
              => Double.Parse(
                  string.IsNullOrEmpty(from) ? "0" : (from ?? "0"),
                  NumberStyles.Any,
                  CultureInfo.InvariantCulture
              ),
            _ => throw new NotSupportedException($"{to?.Name} is not supported for conversion")
        };
    }

    private DateOnly ToDateOnly(string? from)
    {
        if (from == null)
            throw new ArgumentNullException($"ToDateOnly: {nameof(from)} is null");

        if (DateOnly.TryParse(from, out var result))
            return result;

        string value = from ?? "";
        bool european = value.IndexOf('/') > 0;

        string[] v = Regex.Split(value, "/|-");
        //string[] v = from?.Split("-") ?? new string[8];

        if (!Int32.TryParse(string.Join("", european ? v[2] : v[0]), out var year))
            throw new FormatException($"Malformed Date for year {from} :{string.Join("", v[0])}");
        if (!Int32.TryParse(string.Join("", v[1]), out var month))
            throw new FormatException($"Malformed Date for month {from} :{string.Join("", v[1])}");
        if (!Int32.TryParse(string.Join("", european ? v[0] : v[2]), out var day))
            throw new FormatException($"Malformed Date for day {from} :{string.Join("", v[2])}");
        return new DateOnly(year, month, day);
    }

    public async Task<Result> Start(CancellationToken cancellationToken = default)
    {
        Logger?.LogTrace("Start csv reader process");
        if (string.IsNullOrEmpty(Filename))
            return Result.Failure(new Error("You must specify a filename"));
        if (!File.Exists(Filename))
            return Result.Failure(new Error($"{Filename} dit not exists"));

        List<Error> exceptions = new List<Error>();
        using (StreamReader reader = new StreamReader(Filename))
        {
            Logger?.LogTrace("Start reading");
            bool skipped = false;
            LineRead = 0;
            while (!reader.EndOfStream)
            {
                try
                {
                    var line = await reader.ReadLineAsync(cancellationToken);
                    if (line is null)
                        continue;
                    Logger?.LogTrace("Read line:{Line}", line);
                    if (HasHeader && !skipped)
                    {
                        skipped = true;
                        continue;
                    }
                    var v = _onReadLineMapping(line, Separator);
                    LineRead++;

                    _onReadLine.OnNext(new ReaderResponse<T>(LineRead, v, this));
                }
                catch (ReaderColumnSetException ex)
                {
                    Logger?.LogWarning(ex.Message);
                    var error = new Error($"Line {LineRead}-{ex.Message}", ex);
                    exceptions.Add(error);
                }
                catch (Exception ex)
                {
                    Logger?.LogWarning(ex.Message);
                    _onReadLine.OnError(ex);
                }
            }
        }
        //_onReadLine.OnCompleted();
        if (exceptions.Any())
        {
            var res = Result.ValidWithFailure(exceptions.ToArray());
            return res;
        }
        return Result.Success();
    }
}
