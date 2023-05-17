namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseClassifierQuery))]
public class BrowseClassifierQuery : IBrowseClassifierQuery { }
