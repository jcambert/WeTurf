using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatOfPredictedQuery:IQuery<BrowseResultatOfPredictedResponse>
{
}
public sealed record BrowseResultatOfPredictedResponse(List<ResultatOfPredictedDto> Resultats):Response;