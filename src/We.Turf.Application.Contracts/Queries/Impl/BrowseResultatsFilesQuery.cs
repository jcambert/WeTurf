namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatsFilesQuery))]
public class BrowseResultatsFilesQuery : IBrowseResultatsFilesQuery
{
    public string Path { get; set; }
}
