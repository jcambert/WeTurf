using System.Collections.Generic;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatOfPredictedQuery:IRequest<BrowseResultatOfPredictedResponse>
{
}
public sealed record BrowseResultatOfPredictedResponse(List<ResultatOfPredictedDto> Resultats);