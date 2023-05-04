using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionPerClassifierQuery
    : WeM.IQuery<BrowsePredictionPerClassifierResponse> { }

public sealed record BrowsePredictionPerClassifierResponse(
    List<PredictionPerClassifierDto> PredictionPerClassifier
) : WeM.Response;
