namespace We.Turf.Handlers;

public class BrowseResultatsFilesHandler
    : AbpHandler.With<BrowseResultatsFilesQuery, BrowseResultatsFilesResponse>
{
    public BrowseResultatsFilesHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

    protected override async Task<Result<BrowseResultatsFilesResponse>> InternalHandle(
        BrowseResultatsFilesQuery request,
        CancellationToken cancellationToken
    )
    {
        var files = Directory.EnumerateFiles(request.Path, "resultats_*.csv");
        if (!files.Any())
            return Result.Failure<BrowseResultatsFilesResponse>(
                $"{request.Path} resultats_*.Csv doesn't exists"
            );
        return Result.Success<BrowseResultatsFilesResponse>(new(files.ToList()));
    }
}
