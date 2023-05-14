namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatQuery))]
public class BrowseResultatQuery : IBrowseResultatQuery
{
    public DateOnly? Date { get; set; }
}
