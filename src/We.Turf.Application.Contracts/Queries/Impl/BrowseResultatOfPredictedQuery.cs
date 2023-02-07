namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatOfPredictedQuery))]
public class BrowseResultatOfPredictedQuery: IBrowseResultatOfPredictedQuery
{
}
