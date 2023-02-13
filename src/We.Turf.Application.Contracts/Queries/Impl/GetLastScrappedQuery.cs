namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetLastScrappedQuery))]
public class GetLastScrappedQuery: IGetLastScrappedQuery
{
}
