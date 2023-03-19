using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionPerClassifierQuery:IQuery<BrowsePredictionPerClassifierResponse>
{
}
public sealed record BrowsePredictionPerClassifierResponse(List<PredictionPerClassifierDto> PredictionPerClassifier):Response;