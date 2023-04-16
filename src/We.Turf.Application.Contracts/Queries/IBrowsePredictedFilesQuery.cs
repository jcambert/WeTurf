using We.Mediatr;

namespace We.Turf.Queries;

public  interface IBrowsePredictedFilesQuery:IQuery<BrowsePredictedFilesResponse>
{
    string Filename { get; set; }
}

public sealed record BrowsePredictedFilesResponse(string File):Response;
