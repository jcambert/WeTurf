using System.IO;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class BrowseToPredictFilesHandler : AbpHandler.With<BrowseToPredictFilesQuery, BrowseToPredictFilesResponse>
{
    public BrowseToPredictFilesHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

#if MEDIATOR
    public override ValueTask<Result<BrowseToPredictFilesResponse>> Handle(BrowseToPredictFilesQuery request, CancellationToken cancellationToken)
#else
    public override Task<Result<BrowseToPredictFilesResponse>> Handle(BrowseToPredictFilesQuery request, CancellationToken cancellationToken)
#endif
    {
        var files = Directory.EnumerateFiles(request.Path, "*.csv");
        if (!files.Any())
            return Result.Failure<BrowseToPredictFilesResponse>($"{request.Path} *.Csv doesn't exists");
        return Result.Success<BrowseToPredictFilesResponse>(new(files.ToList()));

    }
}
