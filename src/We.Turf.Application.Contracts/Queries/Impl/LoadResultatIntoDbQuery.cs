namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ILoadResultatIntoDbQuery))]
public class LoadResultatIntoDbQuery : ILoadResultatIntoDbQuery
{
    public string Filename { get; set; }
}
