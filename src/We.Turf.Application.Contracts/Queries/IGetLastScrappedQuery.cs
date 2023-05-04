using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetLastScrappedQuery : WeM.IQuery<GetLastScrappedResponse> { }

public sealed record GetLastScrappedResponse(LastScrappedDto LastScrapped) : WeM.Response;
