using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionPerClassifierQuery:IRequest<BrowsePredictionPerClassifierResponse>
{
}
public sealed record BrowsePredictionPerClassifierResponse(List<PredictionPerClassifierDto> PredictionPerClassifier);