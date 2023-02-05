namespace We.Turf;

public interface IPmuServiceAppService:IApplicationService
{
    Task<LoadToPredictIntoDatabaseResponse> LoadToPredictIntoDatabase(LoadToPredictIntoDatabaseQuery query);
}
