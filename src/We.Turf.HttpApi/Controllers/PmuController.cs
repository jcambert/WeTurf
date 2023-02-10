using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using We.Turf.Queries;

namespace We.Turf.Controllers;

public class PmuController : TurfController, IPmuServiceAppService
{
    protected readonly IPmuServiceAppService pmuService;
    public PmuController(IPmuServiceAppService service)
    => (pmuService) = (service);

    [HttpGet]
    public Task<BrowseAccuracyOfClassifierResponse> BrowseAccuracyOfClassifierResponse(BrowseAccuracyOfClassifierQuery query)
    =>pmuService.BrowseAccuracyOfClassifierResponse(query);

    [HttpGet]
    public Task<BrowsePredictionPerClassifierResponse> BrowsePredictionPerClassifier(BrowsePredictionPerClassifierQuery query)
    =>pmuService.BrowsePredictionPerClassifier(query);

    [HttpGet]
    public Task<BrowseResultatOfPredictedResponse> BrowseResultatOfPredicted(BrowseResultatOfPredictedQuery query)
    =>pmuService.BrowseResultatOfPredicted(query);

    [HttpGet]
    public Task<BrowseResultatPerClassifierResponse> BrowseResultatPerClassifier(BrowseResultatPerClassifierQuery query)
    => pmuService.BrowseResultatPerClassifier(query);

    public Task<LoadPredictedIntoDbResponse> LoadPredictedIntoDb(LoadPredictedIntoDbQuery query)
    =>pmuService.LoadPredictedIntoDb(query);

    public Task<LoadResultatIntoDbResponse> LoadResultatIntoDb(LoadResultatIntoDbQuery query)
    => pmuService.LoadResultatIntoDb(query);

    public Task<LoadToPredictIntoDatabaseResponse> LoadToPredictIntoDatabase(LoadToPredictIntoDatabaseQuery query)
    =>pmuService.LoadToPredictIntoDatabase(query);
}
