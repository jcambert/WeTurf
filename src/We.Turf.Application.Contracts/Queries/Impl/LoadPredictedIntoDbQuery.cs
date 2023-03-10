namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ILoadPredictedIntoDbQuery))]
public class LoadPredictedIntoDbQuery : ILoadPredictedIntoDbQuery
{
    public string Filename { get; set; } 
}
