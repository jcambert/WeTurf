namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowsePredictedFilesQuery))]
public class BrowsePredictedFilesQuery : IBrowsePredictedFilesQuery
{
    public string Filename { get; set; }
}
