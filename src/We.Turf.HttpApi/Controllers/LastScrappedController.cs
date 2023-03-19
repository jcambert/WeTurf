/*using Microsoft.AspNetCore.Mvc;
using We.Results;
using We.Turf.Queries;

namespace We.Turf.Controllers;

//[ApiController]
//[Area("pmu")]
//[Route("/last_scrapped/[action]")]

public class LastScrappedController:TurfController,IPmuLastScrappedAppService
{
	private readonly IPmuLastScrappedAppService _appService;
	public LastScrappedController(IPmuLastScrappedAppService appService)
	{
		_appService=appService; ;
	}

	[HttpGet]
    public Task<Result<GetLastScrappedResponse>> Get(GetLastScrappedQuery query)
    =>_appService.Get(query);
}*/
