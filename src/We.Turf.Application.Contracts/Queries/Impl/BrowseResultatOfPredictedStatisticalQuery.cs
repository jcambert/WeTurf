namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatOfPredictedStatisticalQuery))]
public class BrowseResultatOfPredictedStatisticalQuery : IBrowseResultatOfPredictedStatisticalQuery
{
    public DateOnly? Date { get; set; }

    public string? Classifier { get; set; }
    public TypePari Pari { get; set; } = TypePari.Tous;

    public string PariAsString =>
        Pari switch
        {
            TypePari.Simple => "E_SIMPLE_PLACE",
            TypePari.Gagnant => "E_SIMPLE_GAGNANT",
            _ => ""
        };
}
