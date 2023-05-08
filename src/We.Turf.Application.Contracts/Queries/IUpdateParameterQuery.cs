using We.Turf.Entities;

namespace We.Turf.Queries;

internal interface IUpdateParameterQuery : WeM.IQuery<UpdateParameterResponse>
{
    ParameterDto Parameter { get; set; }
}

public sealed record UpdateParameterResponse(ParameterDto Parameter) : WeM.Response;
