using System.Security.Cryptography.X509Certificates;
using We.Results;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace We.Turf;

public class PmuScrapAppService :TurfAppService, IPmuScrapAppService
{
    public Task<Result<BrowseCourseFilesResponse>> BrowseCourseFiles(BrowseCourseFilesQuery query)
    => Mediator.Send(query);

    public Task<Result<BrowsePredictedFilesResponse>> BrowsePredictedFiles(BrowsePredictedFilesQuery query)
    => Mediator.Send(query);

    public Task<Result<BrowseResultatsFilesResponse>> BrowseResultatsFiles(BrowseResultatsFilesQuery query)
    =>Mediator.Send(query);

    public Task<Result<BrowseToPredictFilesResponse>> BrowseToPredictFiles(BrowseToPredictFilesQuery query)
    => Mediator.Send(query);

    public Task<Result<GetLastScrappedResponse>> GetLastScrapped(GetLastScrappedQuery query)
    => Mediator.Send(query);

    public Task<Result<PredictResponse>> Predict(PredictQuery query)
    => Mediator.Send(query);

    public Task<Result<ResultatResponse>> Resultats(ResultatQuery query)
    => Mediator.Send(query);

    public  Task<Result<ScrapResponse>> Scrap(ScrapQuery query)
    => Mediator.Send(query);


}
