namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetProgrammeCourseQuery))]
public class GetProgrammeCourseQuery : IGetProgrammeCourseQuery
{
    public Guid Id { get; set; }
}
