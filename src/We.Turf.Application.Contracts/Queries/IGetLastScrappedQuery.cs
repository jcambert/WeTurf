using We.Results;
using We.Turf.Entities;
using We.AbpExtensions;
namespace We.Turf.Queries;

public interface  IGetLastScrappedQuery:IRequest<Result< GetLastScrappedResponse>>
{
}

public sealed record GetLastScrappedResponse(LastScrappedDto LastScrapped):Response;