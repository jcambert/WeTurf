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
    protected readonly IPmuServiceAppService pmuService;

    public PmuController(IPmuServiceAppService service) => (pmuService) = (service);

    [HttpGet]
    public Task<Result<BrowseAccuracyOfClassifierResponse>> BrowseAccuracyOfClassifier(
        [FromQuery] BrowseAccuracyOfClassifierQuery query
    ) => pmuService.BrowseAccuracyOfClassifier(query);

    [HttpGet]
    public Task<Result<BrowseClassifierResponse>> BrowseClassifier(
        [FromQuery] BrowseClassifierQuery query
    ) => pmuService.BrowseClassifier(query);

    [HttpGet]
    public Task<Result<BrowseDisciplineResponse>> BrowseDiscipline(
        [FromQuery] BrowseDisciplineQuery query
    ) => pmuService.BrowseDiscipline(query);

    [HttpGet]
    public Task<Result<BrowsePredictionResponse>> BrowsePrediction(
        [FromQuery] BrowsePredictionQuery query
    ) => pmuService.BrowsePrediction(query);

    [HttpGet]
    public Task<Result<BrowsePredictionByDateResponse>> BrowsePredictionBydate(
        [FromQuery] BrowsePredictionByDateQuery query
    ) => pmuService.BrowsePredictionBydate(query);

    [HttpGet]
    public Task<Result<BrowsePredictedOnlyResponse>> BrowsePredictionOnly(
        [FromQuery] BrowsePredictedOnlyQuery query
    ) => pmuService.BrowsePredictionOnly(query);

    [HttpGet]
    public Task<Result<BrowsePredictionPerClassifierResponse>> BrowsePredictionPerClassifier(
        [FromQuery] BrowsePredictionPerClassifierQuery query
    ) => pmuService.BrowsePredictionPerClassifier(query);

    [HttpGet]
    public Task<Result<BrowseProgrammeCourseResponse>> BrowseProgrammeCourse(
        [FromQuery] BrowseProgrammeCourseQuery query
    ) => pmuService.BrowseProgrammeCourse(query);

    [HttpGet]
    public Task<Result<BrowseProgrammeReunionResponse>> BrowseProgrammeReunion(
        [FromQuery] BrowseProgrammeReunionQuery query
    ) => pmuService.BrowseProgrammeReunion(query);

    [HttpGet]
    public Task<Result<BrowseResultatResponse>> BrowseResultat(
        [FromQuery] BrowseResultatQuery query
    ) => pmuService.BrowseResultat(query);

    [HttpGet]
    public Task<Result<BrowseResultatOfPredictedResponse>> BrowseResultatOfPredicted(
        [FromQuery] BrowseResultatOfPredictedQuery query
    ) => pmuService.BrowseResultatOfPredicted(query);

    [HttpGet]
    public Task<
        Result<BrowseResultatOfPredictedStatisticalResponse>
    > BrowseResultatOfPredictedStatistical(
        [FromQuery] BrowseResultatOfPredictedStatisticalQuery query
    ) => pmuService.BrowseResultatOfPredictedStatistical(query);

    [HttpGet]
    public Task<
        Result<BrowseResultatOfPredictedWithoutClassifierResponse>
    > BrowseResultatOfPredictedWithoutClassifier(
        [FromQuery] BrowseResultatOfPredictedWithoutClassifierQuery query
    ) => pmuService.BrowseResultatOfPredictedWithoutClassifier(query);

    [HttpGet]
    public Task<Result<BrowseResultatPerClassifierResponse>> BrowseResultatPerClassifier(
        [FromQuery] BrowseResultatPerClassifierQuery query
    ) => pmuService.BrowseResultatPerClassifier(query);

    [HttpGet]
    public Task<Result<GetMiseResponse>> GetMise([FromQuery] GetMiseQuery query) =>
        pmuService.GetMise(query);

    [HttpGet]
    public Task<Result<GetProgrammeCourseResponse>> GetProgrammeCourse(
        [FromQuery] GetProgrammeCourseQuery query
    ) => pmuService.GetProgrammeCourse(query);

    [HttpGet]
    public Task<Result<GetStatistiqueResponse>> GetStatistics(
        [FromQuery] GetStatistiqueQuery query
    ) => pmuService.GetStatistics(query);

    [HttpGet]
    public Task<Result<GetStatistiqueWithDateResponse>> GetStatisticsWithDate(
        [FromQuery] GetStatistiqueWithDateQuery query
    ) => pmuService.GetStatisticsWithDate(query);

    [HttpPost]
    public Task<Result<LoadCourseIntoDbResponse>> LoadCourseIntoDb(LoadCourseIntoDbQuery query) =>
        pmuService.LoadCourseIntoDb(query);

    [HttpPost]
    public Task<Result<LoadOutputFolderIntoDbResponse>> LoadOutputFolderIntoDb(
        LoadOutputFolderIntoDbQuery query
    ) => pmuService.LoadOutputFolderIntoDb(query);

    [HttpPost]
    public Task<Result<LoadPredictedIntoDbResponse>> LoadPredictedIntoDb(
        LoadPredictedIntoDbQuery query
    ) => pmuService.LoadPredictedIntoDb(query);

    [HttpPost]
    public Task<Result<LoadResultatIntoDbResponse>> LoadResultatIntoDb(
        LoadResultatIntoDbQuery query
    ) => pmuService.LoadResultatIntoDb(query);

    [HttpPost]
    public Task<Result<LoadToPredictIntoDatabaseResponse>> LoadToPredictIntoDatabase(
        LoadToPredictIntoDbQuery query
    ) => pmuService.LoadToPredictIntoDatabase(query);
}
