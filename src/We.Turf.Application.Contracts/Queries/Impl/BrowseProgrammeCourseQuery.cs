namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseProgrammeCourseQuery))]
public class BrowseProgrammeCourseQuery : IBrowseProgrammeCourseQuery
{
    public DateOnly Date { get; set; }=DateOnly.FromDateTime(DateTime.Now);
}
