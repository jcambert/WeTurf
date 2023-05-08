using We.Results;

namespace We.Turf;

public interface IPmuScrapAppService : IApplicationService
{
    Task<Result<GetLastScrappedResponse>> GetLastScrapped(GetLastScrappedQuery query);

    Task<Result<ScrapResponse>> Scrap(ScrapQuery query);

    Task<Result<PredictResponse>> Predict(PredictQuery query);

    Task<Result<ResultatResponse>> Resultats(ResultatQuery query);

    Task<Result<BrowseToPredictFilesResponse>> BrowseToPredictFiles(
        BrowseToPredictFilesQuery query
    );

    Task<Result<BrowsePredictedFilesResponse>> BrowsePredictedFiles(
        BrowsePredictedFilesQuery query
    );

    Task<Result<BrowseCourseFilesResponse>> BrowseCourseFiles(BrowseCourseFilesQuery query);

    Task<Result<BrowseResultatsFilesResponse>> BrowseResultatsFiles(
        BrowseResultatsFilesQuery query
    );

    Task<Result<GetParameterResponse>> GetParameter(GetParameterQuery query);

    Task<Result<UpdateParameterResponse>> UpdateParameter(UpdateParameterQuery query);
}
