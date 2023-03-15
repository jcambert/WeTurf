using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatOfPredictedQuery:IRequest<Result< BrowseResultatOfPredictedResponse>>
{
}
public sealed record BrowseResultatOfPredictedResponse(List<ResultatOfPredictedDto> Resultats):Response;