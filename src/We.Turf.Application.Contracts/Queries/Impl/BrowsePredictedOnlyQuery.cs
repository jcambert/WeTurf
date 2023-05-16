namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowsePredictedOnlyQuery))]
public class BrowsePredictedOnlyQuery : IBrowsePredictedOnlyQuery
{
    public DateOnly? Date { get; set; }
}
