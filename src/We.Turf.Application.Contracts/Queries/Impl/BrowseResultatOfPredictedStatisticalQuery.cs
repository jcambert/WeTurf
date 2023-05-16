namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatOfPredictedStatisticalQuery))]
public class BrowseResultatOfPredictedStatisticalQuery : IBrowseResultatOfPredictedStatisticalQuery
{
    public DateOnly? Date { get; set; }
}
