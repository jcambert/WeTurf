using System.IO;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class BrowseResultatsFilesHandler
    : AbpHandler.With<BrowseResultatsFilesQuery, BrowseResultatsFilesResponse>
{
    public BrowseResultatsFilesHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }
#if MEDIATOR
    public override ValueTask<Result<BrowseResultatsFilesResponse>> Handle(
        BrowseResultatsFilesQuery request,
        CancellationToken cancellationToken
    )
#else
    public override Task<Result<BrowseResultatsFilesResponse>> Handle(
        BrowseResultatsFilesQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var files = Directory.EnumerateFiles(request.Path, "resultats_*.csv");
        if (!files.Any())
            return Result.Failure<BrowseResultatsFilesResponse>(
                $"{request.Path} resultats_*.Csv doesn't exists"
            );
        return Result.Success<BrowseResultatsFilesResponse>(new(files.ToList()));
    }
}
