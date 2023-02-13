using We.Turf.Entities;

namespace We.Turf.Queries;

public interface  IGetLastScrappedQuery:IRequest<GetLastScrappedResponse>
{
}

public sealed record GetLastScrappedResponse(LastScrappedDto LastScrapped);