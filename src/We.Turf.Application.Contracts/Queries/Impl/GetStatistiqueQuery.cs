using System.Text.Json.Serialization;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetStatistiqueQuery))]
public class GetStatistiqueQuery : IGetStatistiqueQuery
{
    public string Classifier { get; set; }
    public TypePari Pari { get; set; }
    public bool IncludeNonArrive { get; set; }

    [JsonIgnore]
    public string PariAsString => Pari.AsString();
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetStatistiqueWithDateQuery))]
public class GetStatistiqueWithDateQuery : IGetStatistiqueWithDateQuery
{
    public DateOnly? Start { get; set; }
    public DateOnly? End { get; set; }
    public string Classifier { get; set; }
    public TypePari Pari { get; set; }

    public bool IncludeNonArrive { get; set; }

    [JsonIgnore]
    public string PariAsString => Pari.AsString();
}
