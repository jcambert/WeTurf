namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowsePredictionPerClassifierQuery))]
public class BrowsePredictionPerClassifierQuery : IBrowsePredictionPerClassifierQuery { }
