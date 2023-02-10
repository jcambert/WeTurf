namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseAccuracyOfClassifierQuery))]
public class BrowseAccuracyOfClassifierQuery: IBrowseAccuracyOfClassifierQuery
{
}
