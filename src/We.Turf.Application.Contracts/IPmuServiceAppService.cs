namespace We.Turf;

public interface IPmuServiceAppService:IApplicationService
{
    Task<LoadToPredictIntoDatabaseResponse> LoadToPredictIntoDatabase(LoadToPredictIntoDatabaseQuery query);

    Task<LoadPredictedIntoDbResponse> LoadPredictedIntoDb(LoadPredictedIntoDbQuery query);

    Task<LoadResultatIntoDbResponse> LoadResultatIntoDb(LoadResultatIntoDbQuery query);

    Task<BrowseResultatOfPredictedResponse> BrowseResultatOfPredicted(BrowseResultatOfPredictedQuery query);

    Task<BrowsePredictionPerClassifierResponse> BrowsePredictionPerClassifier(BrowsePredictionPerClassifierQuery query);  
    
    Task<BrowseResultatPerClassifierResponse> BrowseResultatPerClassifier(BrowseResultatPerClassifierQuery query);    

    Task<BrowseAccuracyOfClassifierResponse> BrowseAccuracyOfClassifierResponse(BrowseAccuracyOfClassifierQuery query);
}
