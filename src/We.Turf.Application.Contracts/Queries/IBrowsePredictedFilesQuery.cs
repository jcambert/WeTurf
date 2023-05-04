namespace We.Turf.Queries;

public interface IBrowsePredictedFilesQuery : WeM.IQuery<BrowsePredictedFilesResponse>
{
    string Filename { get; set; }
}

public sealed record BrowsePredictedFilesResponse(string File) : WeM.Response;
