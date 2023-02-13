using System.Threading.Tasks;
using We.Turf.Queries;

namespace We.Turf.Controllers;

public class LastScrappedController:TurfController,IPmuLastScrappedAppService
{
	private readonly IPmuLastScrappedAppService _appService;
	public LastScrappedController(IPmuLastScrappedAppService appService)
	{
		_appService=appService; ;
	}

    public Task<GetLastScrappedResponse> Get(GetLastScrappedQuery query)
    =>_appService.Get(query);
}
