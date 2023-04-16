namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IPredictQuery))]
public class PredictQuery : IPredictQuery
{
    public bool DeleteFilesIfExists { get; set; }
}
