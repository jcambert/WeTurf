namespace We.Csv;

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
