using System.Text.Json.Serialization;

namespace We.Turf.Service;

public class LastScrappedHeader
{
    [JsonPropertyName("lastScrapped")]
    public LastScrapped LastScrapped { get; set; }
}

[Serializable]

public class LastScrapped
{
    [JsonPropertyName("lastDate")]
    public DateTime LastDate { get; set; }
}
