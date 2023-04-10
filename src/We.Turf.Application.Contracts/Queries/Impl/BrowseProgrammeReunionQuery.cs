namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseProgrammeReunionQuery))]
public class BrowseProgrammeReunionQuery : IBrowseProgrammeReunionQuery
{
    public DateOnly Date { get; set; }
}
