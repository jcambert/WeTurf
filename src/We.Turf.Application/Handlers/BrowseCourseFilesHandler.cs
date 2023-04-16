using System.IO;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class BrowseCourseFilesHandler : AbpHandler.With<BrowseCourseFilesQuery, BrowseCourseFilesResponse>
{
    public BrowseCourseFilesHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override Task<Result<BrowseCourseFilesResponse>> Handle(BrowseCourseFilesQuery request, CancellationToken cancellationToken)
    {
        var res = File.Exists(request.Filename);
        if (!res)
        {
            return Result.Failure<BrowseCourseFilesResponse>($"{request.Filename} doesn't exists");
        }
        return Result.Success<BrowseCourseFilesResponse>(new(request.Filename));
    }
}
