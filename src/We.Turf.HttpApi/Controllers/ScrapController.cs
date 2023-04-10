using Microsoft.AspNetCore.Mvc;
using We.Results;
using We.Turf.Queries;

namespace We.Turf.Controllers;

[Produces("application/json")]
[Area("pmu")]
[Route("api/scrap/[action]")]
[ApiVersion("1.0")]
[ApiController]
[ControllerName("Scrap")]



public class ScrapController:TurfController,IPmuScrapAppService
{
	private readonly IPmuScrapAppService _appService;
	public ScrapController(IPmuScrapAppService appService)
	{
		_appService=appService; ;
	}

	[HttpGet]
    public Task<Result<GetLastScrappedResponse>> GetLastScrapped([FromQuery] GetLastScrappedQuery query)
    =>_appService.GetLastScrapped(query);

    [HttpGet]
    public Task<Result<ScrapResponse>> Scrap(ScrapQuery query)
    => _appService.Scrap(query);
}
