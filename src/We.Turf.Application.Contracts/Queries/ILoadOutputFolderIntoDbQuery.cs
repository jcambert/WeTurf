namespace We.Turf.Queries;

public interface ILoadOutputFolderIntoDbQuery : WeM.IQuery<LoadOutputFolderIntoDbResponse>
{
    string Folder { get; set; }
}

public sealed record LoadOutputFolderIntoDbResponse() : WeM.Response;
