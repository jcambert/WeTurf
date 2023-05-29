namespace We.Turf.Handlers;

public class BrowseToPredictFilesHandler
    : AbpHandler.With<BrowseToPredictFilesQuery, BrowseToPredictFilesResponse>
{
    public BrowseToPredictFilesHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

    protected override async Task<Result<BrowseToPredictFilesResponse>> InternalHandle(
        BrowseToPredictFilesQuery request,
        CancellationToken cancellationToken
    )
    {
        await Task.Delay(5);
        var files = Directory.EnumerateFiles(request.Path, "*.csv");
        if (!files.Any())
            return Result.Failure<BrowseToPredictFilesResponse>(
                $"{request.Path} *.Csv doesn't exists"
            );
        return Result.Success<BrowseToPredictFilesResponse>(new(files.ToList()));
    }
}
