using We.Mediatr;

namespace We.Turf.Queries;

public interface ILoadToPredictIntoDbQuery:IQuery<LoadToPredictIntoDatabaseResponse>
{
}

public sealed record LoadToPredictIntoDatabaseResponse():Response;
