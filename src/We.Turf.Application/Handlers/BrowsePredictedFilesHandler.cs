using System.IO;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class BrowsePredictedFilesHandler : AbpHandler.With<BrowsePredictedFilesQuery, BrowsePredictedFilesResponse>
{
    public BrowsePredictedFilesHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
#if MEDIATOR
    public override ValueTask<Result<BrowsePredictedFilesResponse>> Handle(BrowsePredictedFilesQuery request, CancellationToken cancellationToken)
#else
    public override Task<Result<BrowsePredictedFilesResponse>> Handle(BrowsePredictedFilesQuery request, CancellationToken cancellationToken)
#endif
    {
        var res = File.Exists(request.Filename);
        if(!res)
        {
            return Result.Failure<BrowsePredictedFilesResponse>($"{request.Filename} doesn't exists");
        }
        return Result.Success<BrowsePredictedFilesResponse>(new(request.Filename));
    }
}
