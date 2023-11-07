namespace We.Results;

/// <summary>
/// A failure Result
/// </summary>
public sealed class Failure : Result
{
    internal Failure(params Error[] errors) : base(false, errors) { }

    internal Failure(params Exception[] exceptions) : this(Error.Create(exceptions)) { }
}

/// <summary>
/// A Failure Result
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Failure<T> : Result<T>, IFailure
{
    internal Failure() : base(default) { }

    internal Failure(T value,params Error[] errors) : base(value,false,errors) { }

    internal Failure(T value,params Exception[] errors) : this(value,Error.Create( errors)) { }


    internal Failure(params Error[] errors) : this(default, errors) { }

    internal Failure(params Exception[] exceptions) : this(Error.Create(exceptions)) { }
}
