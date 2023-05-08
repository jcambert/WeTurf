using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetParameterQuery : WeM.IQuery<GetParameterResponse> { }

public sealed record GetParameterResponse(ParameterDto Parameter) : WeM.Response;
