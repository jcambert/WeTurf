using We.Mediatr;

namespace We.Turf.Queries;

public interface ILoadOutputFolderIntoDbQuery : IQuery<LoadOutputFolderIntoDbResponse>
{
    string Folder { get; set; }
}

public sealed record LoadOutputFolderIntoDbResponse() : Response;
