namespace We.Results;

public sealed class Valid : Result
{
    internal Valid() : base(true,new string[] { })
    {
    }
}
public sealed class Valid<T> : Result<T>,IValid 
{
    internal Valid(T result) : base(result)
    {

    }
    public Valid():base(default)
    {
        
    }

}
