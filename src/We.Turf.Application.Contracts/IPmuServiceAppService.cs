using We.Results;

namespace We.Turf;

public interface IPmuServiceAppService : IApplicationService
{
    Task<Result<BrowseClassifierResponse>> BrowseClassifier(BrowseClassifierQuery query);
    Task<Result<BrowseProgrammeReunionResponse>> BrowseProgrammeReunion(
        BrowseProgrammeReunionQuery query
    );
    Task<Result<GetProgrammeCourseResponse>> GetProgrammeCourse(GetProgrammeCourseQuery query);
    Task<Result<BrowseProgrammeCourseResponse>> BrowseProgrammeCourse(
        BrowseProgrammeCourseQuery query
    );
    Task<Result<LoadOutputFolderIntoDbResponse>> LoadOutputFolderIntoDb(
        LoadOutputFolderIntoDbQuery query
    );
    Task<Result<LoadCourseIntoDbResponse>> LoadCourseIntoDb(LoadCourseIntoDbQuery query);
    Task<Result<LoadToPredictIntoDatabaseResponse>> LoadToPredictIntoDatabase(
        LoadToPredictIntoDbQuery query
    );

    Task<Result<LoadPredictedIntoDbResponse>> LoadPredictedIntoDb(LoadPredictedIntoDbQuery query);

    Task<Result<LoadResultatIntoDbResponse>> LoadResultatIntoDb(LoadResultatIntoDbQuery query);

    Task<Result<BrowseResultatOfPredictedResponse>> BrowseResultatOfPredicted(
        BrowseResultatOfPredictedQuery query
    );
    Task<Result<BrowseResultatOfPredictedStatisticalResponse>> BrowseResultatOfPredictedStatistical(
        BrowseResultatOfPredictedStatisticalQuery query
    );

    Task<Result<BrowsePredictionPerClassifierResponse>> BrowsePredictionPerClassifier(
        BrowsePredictionPerClassifierQuery query
    );

    Task<Result<BrowseResultatPerClassifierResponse>> BrowseResultatPerClassifier(
        BrowseResultatPerClassifierQuery query
    );

    Task<Result<BrowseAccuracyOfClassifierResponse>> BrowseAccuracyOfClassifier(
        BrowseAccuracyOfClassifierQuery query
    );

    Task<Result<BrowsePredictionResponse>> BrowsePrediction(BrowsePredictionQuery query);

    Task<Result<BrowseResultatResponse>> BrowseResultat(BrowseResultatQuery query);

    Task<Result<BrowsePredictedOnlyResponse>> BrowsePredictionOnly(BrowsePredictedOnlyQuery query);

    Task<Result<GetStatistiqueResponse>> GetStatistics(GetStatistiqueQuery query);

    Task<Result<GetStatistiqueWithDateResponse>> GetStatisticsWithDate(
        GetStatistiqueWithDateQuery query
    );
}
