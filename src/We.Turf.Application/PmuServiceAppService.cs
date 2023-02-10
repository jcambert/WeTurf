using Volo.Abp;

namespace We.Turf;


public class PmuServiceAppService : TurfAppService, IPmuServiceAppService
{
    public Task<BrowseAccuracyOfClassifierResponse> BrowseAccuracyOfClassifierResponse(BrowseAccuracyOfClassifierQuery query)
    => Mediator.Send(query);

    public Task<BrowsePredictionPerClassifierResponse> BrowsePredictionPerClassifier(BrowsePredictionPerClassifierQuery query)
    => Mediator.Send(query);

    public Task<BrowseResultatOfPredictedResponse> BrowseResultatOfPredicted(BrowseResultatOfPredictedQuery query)
    => Mediator.Send(query);

    public Task<BrowseResultatPerClassifierResponse> BrowseResultatPerClassifier(BrowseResultatPerClassifierQuery query)
    => Mediator.Send(query);

    public Task<LoadPredictedIntoDbResponse> LoadPredictedIntoDb(LoadPredictedIntoDbQuery query)
    => Mediator.Send(query);

    public Task<LoadResultatIntoDbResponse> LoadResultatIntoDb(LoadResultatIntoDbQuery query)
    => Mediator.Send(query);

    public Task<LoadToPredictIntoDatabaseResponse> LoadToPredictIntoDatabase(LoadToPredictIntoDatabaseQuery query)
    =>Mediator.Send(query);
}
