using We.Results;

namespace We.Turf;

public class PmuScrapAppService :TurfAppService, IPmuScrapAppService
{
    public Task<Result<GetLastScrappedResponse>> GetLastScrapped(GetLastScrappedQuery query)
    => Mediator.Send(query);

    public Task<Result<ScrapResponse>> Scrap(ScrapQuery query)
    => Mediator.Send(query);
}
