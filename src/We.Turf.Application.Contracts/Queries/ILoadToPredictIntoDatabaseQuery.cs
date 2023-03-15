

using We.AbpExtensions;
using We.Results;

namespace We.Turf.Queries;

public interface ILoadToPredictIntoDatabaseQuery:IRequest<Result<LoadToPredictIntoDatabaseResponse>>
{
}

public sealed record LoadToPredictIntoDatabaseResponse():Response;
