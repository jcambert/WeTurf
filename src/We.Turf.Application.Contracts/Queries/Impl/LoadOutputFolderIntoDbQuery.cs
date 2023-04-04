namespace We.Turf.Queries;
[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ILoadOutputFolderIntoDbQuery))]
public class LoadOutputFolderIntoDbQuery : ILoadOutputFolderIntoDbQuery
{
    private readonly string _defaultFolder;
    public LoadOutputFolderIntoDbQuery()
    {
        
    }
    public string Folder { get; set; }
}
