namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseCourseFilesQuery))]
public class BrowseCourseFilesQuery : IBrowseCourseFilesQuery
{
    public string Filename { get; set; }
}
