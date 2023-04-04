using Microsoft.AspNetCore.Mvc;
using We.Results;
using We.Turf.Queries;

namespace We.Turf.Controllers;
[Produces("application/json")]
[Area("pmu")]
[Route("api/pmu/[action]")]
[ApiVersion("1.0")]
[ApiController]
[ControllerName("Pmu")]

public class PmuController : TurfController, IPmuServiceAppService
{

    /*[HttpGet("hello")]
    public Task<string> GetHello()
    {
        return Task.FromResult("HELLO");
    }*/
    protected readonly IPmuServiceAppService pmuService;
    public PmuController(IPmuServiceAppService service)
    => (pmuService) = (service);

    [HttpGet]
    public Task<Result< BrowseAccuracyOfClassifierResponse>> BrowseAccuracyOfClassifier(BrowseAccuracyOfClassifierQuery query)
    =>pmuService.BrowseAccuracyOfClassifier(query);

    [HttpGet]
    public Task<Result<BrowsePredictionResponse>> BrowsePrediction(BrowsePredictionQuery query)
    =>pmuService.BrowsePrediction(query);   

    [HttpGet]
    public Task<Result<BrowsePredictionPerClassifierResponse>> BrowsePredictionPerClassifier(BrowsePredictionPerClassifierQuery query)
    =>pmuService.BrowsePredictionPerClassifier(query);

    [HttpGet]
    public Task<Result<BrowseResultatOfPredictedResponse>> BrowseResultatOfPredicted(BrowseResultatOfPredictedQuery query)
    =>pmuService.BrowseResultatOfPredicted(query);

    [HttpGet]
    public Task<Result<BrowseResultatPerClassifierResponse>> BrowseResultatPerClassifier(BrowseResultatPerClassifierQuery query)
    => pmuService.BrowseResultatPerClassifier(query);

    [HttpPost]
    public Task<Result<LoadCourseIntoDbResponse>> LoadCourseIntoDb( LoadCourseIntoDbQuery query)
    => pmuService.LoadCourseIntoDb(query);

    [HttpPost]
    public Task<Result<LoadOutputFolderIntoDbResponse>> LoadOutputFolderIntoDb(LoadOutputFolderIntoDbQuery query)
    => pmuService.LoadOutputFolderIntoDb(query);

    [HttpPost]
    public Task<Result<LoadPredictedIntoDbResponse>> LoadPredictedIntoDb(LoadPredictedIntoDbQuery query)
    =>pmuService.LoadPredictedIntoDb(query);

    [HttpPost]
    public Task<Result<LoadResultatIntoDbResponse>> LoadResultatIntoDb(LoadResultatIntoDbQuery query)
    => pmuService.LoadResultatIntoDb(query);

    [HttpPost]
    public Task<Result<LoadToPredictIntoDatabaseResponse>> LoadToPredictIntoDatabase(LoadToPredictIntoDbQuery query)
    =>pmuService.LoadToPredictIntoDatabase(query);
}
