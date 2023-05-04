using We.Mediatr;
using We.Results;

namespace We.Turf;

public class PmuServiceAppService : TurfAppService, IPmuServiceAppService
{
    public Task<Result<BrowseAccuracyOfClassifierResponse>> BrowseAccuracyOfClassifier(
        BrowseAccuracyOfClassifierQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowsePredictionResponse>> BrowsePrediction(BrowsePredictionQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowsePredictionPerClassifierResponse>> BrowsePredictionPerClassifier(
        BrowsePredictionPerClassifierQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseProgrammeCourseResponse>> BrowseProgrammeCourse(
        BrowseProgrammeCourseQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseProgrammeReunionResponse>> BrowseProgrammeReunion(
        BrowseProgrammeReunionQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseResultatOfPredictedResponse>> BrowseResultatOfPredicted(
        BrowseResultatOfPredictedQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseResultatPerClassifierResponse>> BrowseResultatPerClassifier(
        BrowseResultatPerClassifierQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<GetProgrammeCourseResponse>> GetProgrammeCourse(
        GetProgrammeCourseQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<LoadCourseIntoDbResponse>> LoadCourseIntoDb(LoadCourseIntoDbQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<LoadOutputFolderIntoDbResponse>> LoadOutputFolderIntoDb(
        LoadOutputFolderIntoDbQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<LoadPredictedIntoDbResponse>> LoadPredictedIntoDb(
        LoadPredictedIntoDbQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<LoadResultatIntoDbResponse>> LoadResultatIntoDb(
        LoadResultatIntoDbQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<LoadToPredictIntoDatabaseResponse>> LoadToPredictIntoDatabase(
        LoadToPredictIntoDbQuery query
    ) => Mediator.Send(query).AsTaskWrap();
}
