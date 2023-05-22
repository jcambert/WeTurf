namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowsePredictionByDateQuery))]
public class BrowsePredictionByDateQuery : IBrowsePredictionByDateQuery
{
    public DateOnly? Date { get; set; }
    public int? Reunion { get; set; }
    public int? Course { get; set; }
}
