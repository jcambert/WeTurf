namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetDividendeQuery))]
public class GetDividendeQuery : IGetDividendeQuery
{
    public DateOnly Date { get; set; }
    public int? Reunion { get; set; }
    public int? Course { get; set; }
    public string Classifier { get; set; }
    public TypePari Pari { get; set; }
}
