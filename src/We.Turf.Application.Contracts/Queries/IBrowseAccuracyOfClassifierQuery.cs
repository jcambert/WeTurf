using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseAccuracyOfClassifierQuery:IRequest<Result<BrowseAccuracyOfClassifierResponse>>
{
}

public sealed record BrowseAccuracyOfClassifierResponse(List<AccuracyPerClassifierDto> ClassifiersAccuracy):Response;
