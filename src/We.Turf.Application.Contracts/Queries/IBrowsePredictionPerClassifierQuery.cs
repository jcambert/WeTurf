using System.Collections.Generic;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowsePredictionPerClassifierQuery:IRequest<Result<BrowsePredictionPerClassifierResponse>>
{
}
public sealed record BrowsePredictionPerClassifierResponse(List<PredictionPerClassifierDto> PredictionPerClassifier):Response;