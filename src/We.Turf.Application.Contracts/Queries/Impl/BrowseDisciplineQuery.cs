namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseDisciplineQuery))]
public class BrowseDisciplineQuery : IBrowseDisciplineQuery { }
