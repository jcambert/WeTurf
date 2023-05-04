using System.IO;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class BrowseCourseFilesHandler
    : AbpHandler.With<BrowseCourseFilesQuery, BrowseCourseFilesResponse>
{
    public BrowseCourseFilesHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }
#if MEDIATOR
    public override ValueTask<Result<BrowseCourseFilesResponse>> Handle(
        BrowseCourseFilesQuery request,
        CancellationToken cancellationToken
    )
#else
    public override Task<Result<BrowseCourseFilesResponse>> Handle(
        BrowseCourseFilesQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var res = File.Exists(request.Filename);
        if (!res)
        {
            return Result.Failure<BrowseCourseFilesResponse>($"{request.Filename} doesn't exists");
        }
        return Result.Success<BrowseCourseFilesResponse>(new(request.Filename));
    }
}
