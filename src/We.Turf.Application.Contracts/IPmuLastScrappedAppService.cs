using We.Results;

namespace We.Turf;

public interface IPmuLastScrappedAppService:IApplicationService
{
    Task<Result<GetLastScrappedResponse>> Get(GetLastScrappedQuery query);
}
