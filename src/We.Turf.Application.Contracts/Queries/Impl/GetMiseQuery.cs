namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetMiseQuery))]
public class GetMiseQuery : IGetMiseQuery
{
    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public int? Reunion { get; set; }
    public int? Course { get; set; }
}
