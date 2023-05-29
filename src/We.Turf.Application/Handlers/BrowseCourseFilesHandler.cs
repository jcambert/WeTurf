namespace We.Turf.Handlers;

public class BrowseCourseFilesHandler
    : AbpHandler.With<BrowseCourseFilesQuery, BrowseCourseFilesResponse>
{
    public BrowseCourseFilesHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

    protected override Task<Result<BrowseCourseFilesResponse>> InternalHandle(
        BrowseCourseFilesQuery request,
        CancellationToken cancellationToken
    )
    {
        var res = File.Exists(request.Filename);
        if (!res)
        {
            return Result.Failure<BrowseCourseFilesResponse>($"{request.Filename} doesn't exists");
        }
        return Result.Success<BrowseCourseFilesResponse>(new(request.Filename));
    }
}
