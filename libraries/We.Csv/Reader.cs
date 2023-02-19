using System.Globalization;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text.RegularExpressions;

namespace We.Csv;

public record ReaderResponse<T>(int Index, T Value, Reader<T> Reader)
    where T : class, new();
public class Reader<T>
    where T : class, new()
{
    private readonly ISubject<ReaderResponse<T>> _onReadLine = new Subject<ReaderResponse<T>>();
    public IObservable<ReaderResponse<T>> OnReadLine => _onReadLine.AsObservable();
    public string Filename { get; init; }
    public bool HasHeader { get; init; }
    public char Separator { get; init; }
    public int LineRead { get; private set; } = 0;

    private Func<string?, char, T> _onReadLineMapping;
    private readonly List<MapColumn> columns = new List<MapColumn>();

    public Reader(string filename, bool hasHeader, char separator)
    {
        this.Filename = filename;
        this.HasHeader = hasHeader;
        this.Separator = separator;
        _onReadLineMapping = OnReadLineMapping;

        MapColumns();
    }

    private void MapColumns()
    {
        var properties = typeof(T).GetProperties().Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(CsvFieldAttribute)));
        foreach (var property in properties)
        {
            var attr = property.GetCustomAttribute<CsvFieldAttribute>();
            columns.Add(new MapColumn(attr?.Index ?? 0, attr?.Name ?? string.Empty, property));
        }
    }

    protected virtual T OnReadLineMapping(string? line, char separator)
    {
        T result = new T();
        var values = line?.Split(separator) ?? new string[0];
        foreach (var col in columns)
        {
            if (col.Index >= 0 && col.Index <= values.Length)
            {
                var v = values[col.Index];
                var vv = To(v, col?.Property?.GetSetMethod()?.GetParameters()?.First().ParameterType);
                col?.Property?.GetSetMethod()?.Invoke(result, new object?[] { vv });
            }
        }

        return result;
    }
    protected object To(string? from, Type? to)
    {
        return to?.Name switch
        {
            nameof(DateOnly) => ToDateOnly(from),
            nameof(String) => from ?? "",
            nameof(Int32) => Int32.Parse(from?.Split(".")[0] ?? ""),
            nameof(Double) => Double.Parse(from ?? "", NumberStyles.Any, CultureInfo.InvariantCulture),
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
    public async Task Start(CancellationToken cancellationToken = default)
    {

        using (StreamReader reader = new StreamReader(Filename))
        {
            bool skipped = false;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync(cancellationToken);
                if (HasHeader && !skipped)
                {
                    skipped = true;
                    continue;
                }
                var v = _onReadLineMapping(line, Separator);
                LineRead++;

                _onReadLine.OnNext(new ReaderResponse<T>(LineRead, v, this));
            }

        }
        _onReadLine.OnCompleted();


    }

}