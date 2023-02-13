using System.Text.Json.Serialization;

namespace We.Turf.Service;

[Serializable]
public class ScrapOptions
{
    public const string SCRAP_OPTIONS = "Scrapper";
    public bool Enabled { get; set; } = true;

    [JsonConverter(typeof(TimeOnlyConverter))]
    public string Time { get; set; } 
    public TimeOnly TTime =>TimeOnly.Parse(Time);
    public double Tick { get; set; } =60;
    public DateOnly? LastDate { get; internal set; }
}

internal class ScrapConstants
{
    internal const string SCRAPPER_WORKING_DIRECTORY = @"projets\pmu_scrapper";
    internal const string SCRAPPER_PREDICTED = @$"{SCRAPPER_WORKING_DIRECTORY}\output\predicted.csv";
    internal const string SCRAPPER_RESULTAT = $@"{SCRAPPER_WORKING_DIRECTORY}\output\resultats_trot_attele.csv";
}