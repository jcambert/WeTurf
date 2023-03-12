using System.Diagnostics;

namespace We.Results;

public sealed class Failure : Result
{

    internal Failure(params Error[] errors) : base(false, errors)
    {
    }
}

public sealed class Failure<T> : Result<T>, IFailure
{
    public Failure() : base(default)
    {
    }
    internal Failure(params Error[] errors) : base(default, false, errors)
    {

    }

   // public static implicit operator Task<Result>(Failure failure) =>(Task<Result>)Task.FromResult(failure);
}

