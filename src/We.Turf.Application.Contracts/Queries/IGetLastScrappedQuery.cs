using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetLastScrappedQuery : IQuery<GetLastScrappedResponse> { }

public sealed record GetLastScrappedResponse(LastScrappedDto LastScrapped) : Response;
