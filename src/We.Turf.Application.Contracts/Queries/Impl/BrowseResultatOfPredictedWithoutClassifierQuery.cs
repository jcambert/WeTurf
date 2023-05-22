namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatOfPredictedWithoutClassifierQuery))]
public class BrowseResultatOfPredictedWithoutClassifierQuery
    : IBrowseResultatOfPredictedWithoutClassifierQuery
{
    public DateOnly? Date { get; set; }
    public TypePari Pari { get; set; } = TypePari.Place;
    public int? Reunion { get; set; }
    public int? Course { get; set; }
    public bool IncludeNonArrive { get; set; } = true;
}
