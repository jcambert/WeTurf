using We.Mediatr;
using We.Results;

namespace We.Turf;

public class PmuScrapAppService : TurfAppService, IPmuScrapAppService
{
    public Task<Result<BrowseCourseFilesResponse>> BrowseCourseFiles(
        BrowseCourseFilesQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowsePredictedFilesResponse>> BrowsePredictedFiles(
        BrowsePredictedFilesQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseResultatsFilesResponse>> BrowseResultatsFiles(
        BrowseResultatsFilesQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseToPredictFilesResponse>> BrowseToPredictFiles(
        BrowseToPredictFilesQuery query
    ) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<GetLastScrappedResponse>> GetLastScrapped(GetLastScrappedQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<GetParameterResponse>> GetParameter(GetParameterQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<PredictResponse>> Predict(PredictQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<ResultatResponse>> Resultats(ResultatQuery query) =>
        Mediator.Send(query).AsTaskWrap();

    public Task<Result<ScrapResponse>> Scrap(ScrapQuery query) => Mediator.Send(query).AsTaskWrap();

    public Task<Result<UpdateParameterResponse>> UpdateParameter(UpdateParameterQuery query) =>
        Mediator.Send(query).AsTaskWrap();
}
