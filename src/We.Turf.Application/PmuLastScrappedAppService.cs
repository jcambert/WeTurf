using We.Results;

namespace We.Turf;

public class PmuLastScrappedAppService :TurfAppService, IPmuLastScrappedAppService
{
    public Task<Result<GetLastScrappedResponse>> Get(GetLastScrappedQuery query)
    => Mediator.Send(query);
}
