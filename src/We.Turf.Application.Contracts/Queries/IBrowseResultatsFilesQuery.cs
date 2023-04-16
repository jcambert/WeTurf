using We.Mediatr;

namespace We.Turf.Queries;

public interface IBrowseResultatsFilesQuery:IQuery<BrowseResultatsFilesResponse>
{
    string Path { get; set; }
}

public sealed record BrowseResultatsFilesResponse(List<string> Files):Response;