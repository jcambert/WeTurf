namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseToPredictFilesQuery))]
public class BrowseToPredictFilesQuery : IBrowseToPredictFilesQuery
{
    public string Path { get; set; }
}
