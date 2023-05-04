namespace We.Turf.Queries;

public interface IBrowseToPredictFilesQuery : WeM.IQuery<BrowseToPredictFilesResponse>
{
    string Path { get; set; }
}

public sealed record BrowseToPredictFilesResponse(List<string> Files) : WeM.Response;
