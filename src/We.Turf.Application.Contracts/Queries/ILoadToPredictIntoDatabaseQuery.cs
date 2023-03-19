using We.Mediatr;

namespace We.Turf.Queries;

public interface ILoadToPredictIntoDatabaseQuery:IQuery<LoadToPredictIntoDatabaseResponse>
{
}

public sealed record LoadToPredictIntoDatabaseResponse():Response;
