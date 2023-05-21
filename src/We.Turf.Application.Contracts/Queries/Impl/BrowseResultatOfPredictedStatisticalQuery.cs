using System.Text.Json.Serialization;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseResultatOfPredictedStatisticalQuery))]
public class BrowseResultatOfPredictedStatisticalQuery : IBrowseResultatOfPredictedStatisticalQuery
{
    public DateOnly? Date { get; set; }

    public string? Classifier { get; set; }
    public TypePari Pari { get; set; } = TypePari.Tous;

    public int? Reunion { get; set; }

    public int? Course { get; set; }

    [JsonIgnore]
    public string PariAsString => Pari.AsString();
}
