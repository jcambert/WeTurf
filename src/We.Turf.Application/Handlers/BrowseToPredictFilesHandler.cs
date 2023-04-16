using System.IO;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class BrowseToPredictFilesHandler : AbpHandler.With<BrowseToPredictFilesQuery, BrowseToPredictFilesResponse>
{
    public BrowseToPredictFilesHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override Task<Result<BrowseToPredictFilesResponse>> Handle(BrowseToPredictFilesQuery request, CancellationToken cancellationToken)
    {
        var files = Directory.EnumerateFiles(request.Path, "*.csv");
        if (!files.Any())
            return Result.Failure<BrowseToPredictFilesResponse>($"{request.Path} *.Csv doesn't exists");
        return Result.Success<BrowseToPredictFilesResponse>(new(files.ToList()));

    }
}
