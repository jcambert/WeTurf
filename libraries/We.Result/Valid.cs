using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace We.Results;

/// <summary>
/// a Valid Result
/// </summary>
public sealed class Valid : Result
{
    internal Valid() : base(true, new string[] { }) { }
}

/// <summary>
/// A Valid Result
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Valid<T> : Result<T>, IValid
{
    internal Valid(T result) : base(result) { }

    internal Valid() : base(default) { }
}

public sealed class ValidWithFailure : Result, IValidWithFailure
{
    internal ValidWithFailure(params Error[] errors) : base(true, errors) { }

    internal ValidWithFailure(params Exception[] exceptions) : base(true, Error.Create(exceptions))
    { }

    public override IReadOnlyList<Error> Errors => _errors;

    public override bool IsValidFailure => true;
}

public sealed class ValidWithFailure<T> : Result<T>, IValidWithFailure<T>
{
    internal ValidWithFailure(Error error) : base(default, true, new Error[] { error }) { }

    internal ValidWithFailure(Exception exception) : base(default, true, Error.Create(exception))
    { }

    internal ValidWithFailure(T result, params Error[] errors) : base(result, true, errors) { }

    internal ValidWithFailure(T result, params Exception[] exceptions)
        : base(result, true, Error.Create(exceptions)) { }

    public override IReadOnlyList<Error> Errors => _errors;

    public override bool IsValidFailure => true;
}
