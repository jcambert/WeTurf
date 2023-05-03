namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatPerClassifierQuery))]
public class BrowseResultatPerClassifierQuery : IBrowseResultatPerClassifierQuery { }
