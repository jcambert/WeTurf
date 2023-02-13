namespace We.Turf;

public interface IPmuLastScrappedAppService:IApplicationService
{
    Task<GetLastScrappedResponse> Get(GetLastScrappedQuery query);
}
