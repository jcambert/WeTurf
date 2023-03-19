using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseAccuracyOfClassifierQuery:IQuery<BrowseAccuracyOfClassifierResponse>
{
}

public sealed record BrowseAccuracyOfClassifierResponse(List<AccuracyPerClassifierDto> ClassifiersAccuracy):Response;
