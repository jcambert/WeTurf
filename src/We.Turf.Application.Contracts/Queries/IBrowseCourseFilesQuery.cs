using We.Mediatr;

namespace We.Turf.Queries;

public interface IBrowseCourseFilesQuery:IQuery<BrowseCourseFilesResponse>
{
    public  string Filename { get; set; }
}

public sealed record BrowseCourseFilesResponse(string File):Response;
