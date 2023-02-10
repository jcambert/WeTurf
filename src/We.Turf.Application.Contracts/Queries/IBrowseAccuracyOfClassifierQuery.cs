using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseAccuracyOfClassifierQuery:IRequest<BrowseAccuracyOfClassifierResponse>
{
}

public sealed record BrowseAccuracyOfClassifierResponse(List<AccuracyOfClassifierDto> ClassifiersAccuracy);
