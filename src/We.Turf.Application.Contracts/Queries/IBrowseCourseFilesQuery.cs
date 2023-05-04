namespace We.Turf.Queries;

public interface IBrowseCourseFilesQuery : WeM.IQuery<BrowseCourseFilesResponse>
{
    public string Filename { get; set; }
}

public sealed record BrowseCourseFilesResponse(string File) : WeM.Response;
