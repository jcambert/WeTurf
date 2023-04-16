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
    public Task<Result<BrowseCourseFilesResponse>> BrowseCourseFiles([FromQuery] BrowseCourseFilesQuery query)
    =>_appService.BrowseCourseFiles(query);

    [HttpGet]
    public Task<Result<BrowsePredictedFilesResponse>> BrowsePredictedFiles([FromQuery] BrowsePredictedFilesQuery query)
    =>_appService.BrowsePredictedFiles(query);

    [HttpGet]
    public Task<Result<BrowseResultatsFilesResponse>> BrowseResultatsFiles([FromQuery] BrowseResultatsFilesQuery query)
    =>_appService.BrowseResultatsFiles(query);

    [HttpGet]
    public Task<Result<BrowseToPredictFilesResponse>> BrowseToPredictFiles([FromQuery] BrowseToPredictFilesQuery query)
    =>_appService.BrowseToPredictFiles(query);  

    [HttpGet]
    public Task<Result<GetLastScrappedResponse>> GetLastScrapped([FromQuery] GetLastScrappedQuery query)
    =>_appService.GetLastScrapped(query);

    [HttpGet]
    public Task<Result<PredictResponse>> Predict([FromQuery] PredictQuery query)
    =>_appService.Predict(query);

    [HttpGet]
    public Task<Result<ResultatResponse>> Resultats([FromQuery] ResultatQuery query)
    =>_appService.Resultats(query); 

    [HttpGet]
    public Task<Result<ScrapResponse>> Scrap([FromQuery] ScrapQuery query)
    => _appService.Scrap(query);
}
