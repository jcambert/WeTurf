namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ILoadToPredictIntoDbQuery))]
public class LoadToPredictIntoDbQuery : ILoadToPredictIntoDbQuery
{
    public string Filename { get; set; }
    public bool Rename { get; set; }
}
