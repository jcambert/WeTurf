namespace We.Turf.Handlers;

public class BrowsePredictedFilesHandler
    : AbpHandler.With<BrowsePredictedFilesQuery, BrowsePredictedFilesResponse>
{
    public BrowsePredictedFilesHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

    protected override Task<Result<BrowsePredictedFilesResponse>> InternalHandle(
        BrowsePredictedFilesQuery request,
        CancellationToken cancellationToken
    )
    {
        var res = File.Exists(request.Filename);
        if (!res)
        {
            return Result.Failure<BrowsePredictedFilesResponse>(
                $"{request.Filename} doesn't exists"
            );
        }
        return Result.Success<BrowsePredictedFilesResponse>(new(request.Filename));
    }
}
