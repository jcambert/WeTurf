namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ILoadCourseIntoDbQuery))]
public class LoadCourseIntoDbQuery : ILoadCourseIntoDbQuery
{
    public string Filename { get; set; }
}
