namespace We.Turf;

public class PmuLastScrappedAppService :TurfAppService, IPmuLastScrappedAppService
{
    public Task<GetLastScrappedResponse> Get(GetLastScrappedQuery query)
    => Mediator.Send(query);
}
