using We.Mediatr;

namespace We.Turf.Queries;

public interface IBrowseToPredictFilesQuery:IQuery<BrowseToPredictFilesResponse>
{
    string Path { get; set; }
}

public sealed record BrowseToPredictFilesResponse(List<string> Files):Response;