using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictedOnlyQuery : WeM.IQuery<BrowsePredictedOnlyResponse>
{
    DateOnly? Date { get; set; }
}

public sealed record BrowsePredictedOnlyResponse(List<PredictedOnlyDto> Predicteds) : WeM.Response;
