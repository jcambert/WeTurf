
namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ILoadToPredictIntoDatabaseQuery))]
public class LoadToPredictIntoDatabaseQuery: ILoadToPredictIntoDatabaseQuery
{
}
