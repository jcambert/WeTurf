using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionQuery : WeM.IQuery<BrowsePredictionResponse>
{
    DateOnly? Date { get; set; }
    string Classifier { get; set; }
}

public sealed record BrowsePredictionResponse(List<PredictedDto> Predicteds) : WeM.Response;
