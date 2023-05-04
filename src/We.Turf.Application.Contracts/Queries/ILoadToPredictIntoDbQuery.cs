using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadToPredictIntoDbQuery : WeM.IQuery<LoadToPredictIntoDatabaseResponse>
{
    string Filename { get; set; }
    bool Rename { get; set; }
}

public sealed record LoadToPredictIntoDatabaseResponse(List<ToPredictDto> Courses) : WeM.Response;
