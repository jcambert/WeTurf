namespace We.Turf;

public class PmuServiceAppService : TurfAppService, IPmuServiceAppService
{
    public Task<LoadToPredictIntoDatabaseResponse> LoadToPredictIntoDatabase(LoadToPredictIntoDatabaseQuery query)
    =>Mediator.Send(query);
}
