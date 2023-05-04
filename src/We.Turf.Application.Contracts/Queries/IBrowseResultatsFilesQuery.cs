namespace We.Turf.Queries;

public interface IBrowseResultatsFilesQuery : WeM.IQuery<BrowseResultatsFilesResponse>
{
    string Path { get; set; }
}

public sealed record BrowseResultatsFilesResponse(List<string> Files) : WeM.Response;
