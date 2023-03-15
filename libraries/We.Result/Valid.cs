namespace We.Results;

/// <summary>
/// a Valid Result
/// </summary>
public sealed class Valid : Result
{
    internal Valid() : base(true,new string[] { })
    {
    }
}

/// <summary>
/// A Valid Result
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Valid<T> : Result<T>,IValid 
{
    internal Valid(T result) : base(result)
    {

    }
    internal Valid():base(default)
    {
        
    }

}
