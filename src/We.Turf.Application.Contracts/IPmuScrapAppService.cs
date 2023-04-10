using We.Results;

namespace We.Turf;

public interface IPmuScrapAppService:IApplicationService
{
    Task<Result<GetLastScrappedResponse>> GetLastScrapped(GetLastScrappedQuery query);

    Task<Result<ScrapResponse>> Scrap(ScrapQuery query);
}
