using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace We.Csv;

[DebuggerDisplay("{Name}")]
public class TestCsv
{
    public TestCsv()
    {

    }

    [CsvField(1)]
    public string Name { get; set; }

    public override string ToString() => Name;
    
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class CsvFieldAttribute : Attribute
{
    public CsvFieldAttribute(int index) : this(index, string.Empty)
    {

    }
    public CsvFieldAttribute(int index, string name)
    {
        this.Index = index;
        this.Name = name;
    }

    public int Index { get; }
    public string Name { get; }
}
internal record MapColumn(int Index,string Name, PropertyInfo Property);
public class Reader<T>
    where T : class, new()
{
    private readonly ISubject<T> _onReadLine = new Subject<T>();
    public IObservable<T> OnReadLine => _onReadLine.AsObservable();
    public string Filename { get; init; }
    public bool HasHeader { get; init; }
    public char Separator { get; init; }

    private Func<string?,char, T> _onReadLineMapping;
    private readonly List<MapColumn> columns = new List<MapColumn>();

    public Reader(string filename, bool hasHeader, char separator)
    {
        this.Filename = filename;
        this.HasHeader=hasHeader;
        this.Separator=separator;
        _onReadLineMapping = OnReadLineMapping;

        MapColumns();
    }

    private void MapColumns()
    {
        var properties = typeof(T).GetProperties().Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(CsvFieldAttribute)));
        foreach (var property in properties)
        {
            var attr=property.GetCustomAttribute<CsvFieldAttribute>();
            columns.Add(new MapColumn(attr?.Index ?? 0, attr?.Name ?? string.Empty, property));
        }
    }

    protected virtual T OnReadLineMapping(string? line, char separator)
    {
        T result= new T();
        var values = line?.Split(separator) ?? new string[0];
        foreach (var col in columns)
        {
            if (col.Index > 0 && col.Index <= values.Length)
            {
                var v = values[col.Index];
                col?.Property?.GetSetMethod()?.Invoke(result, new object?[] { v });
            }
        }
        
        return result;
    }
    public async Task Start(CancellationToken cancellationToken=default)
    {
        bool _isRunning = false;

        using (StreamReader reader = new StreamReader(Filename))
        {
            _isRunning = true;
            var i = Observable.Using(
                () => reader,
                reader => Observable.FromAsync(reader.ReadLineAsync)
                    .Repeat()
                    .TakeWhile(x => x != null)
                );
            i.Skip(HasHeader?1:0).Subscribe(
                x => _onReadLine.OnNext(_onReadLineMapping(x,Separator)),
                () =>
                {
                    _onReadLine.OnCompleted();
                    _isRunning = false;
                });
        }
        while (_isRunning)
        {
            await Task.Delay(500);
            if (cancellationToken.IsCancellationRequested)
            {
                _onReadLine.OnCompleted();
                _isRunning = false;
            }
        }

    }

}