using Volo.Abp;
using We.Results;

namespace We.Turf;


public class PmuServiceAppService : TurfAppService, IPmuServiceAppService
{

    public  Task<Result<BrowseAccuracyOfClassifierResponse>> BrowseAccuracyOfClassifier(BrowseAccuracyOfClassifierQuery query)
    => Mediator.Send( query);

    public Task<Result<BrowsePredictionResponse>> BrowsePrediction(BrowsePredictionQuery query)
    => Mediator.Send(query);

    public Task<Result< BrowsePredictionPerClassifierResponse>> BrowsePredictionPerClassifier(BrowsePredictionPerClassifierQuery query)
    => Mediator.Send(query);

    public Task<Result<BrowseResultatOfPredictedResponse>> BrowseResultatOfPredicted(BrowseResultatOfPredictedQuery query)
    => Mediator.Send(query);

    public Task<Result<BrowseResultatPerClassifierResponse>> BrowseResultatPerClassifier(BrowseResultatPerClassifierQuery query)
    => Mediator.Send(query);

    public Task<Result<LoadPredictedIntoDbResponse>> LoadPredictedIntoDb(LoadPredictedIntoDbQuery query)
    => Mediator.Send(query);

    public Task<Result<LoadResultatIntoDbResponse>> LoadResultatIntoDb(LoadResultatIntoDbQuery query)
    => Mediator.Send(query);

    public Task<Result< LoadToPredictIntoDatabaseResponse>> LoadToPredictIntoDatabase(LoadToPredictIntoDatabaseQuery query)
    =>Mediator.Send(query);
}
