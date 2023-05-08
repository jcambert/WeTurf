namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IScrapQuery))]
public class ScrapQuery : IScrapQuery
{
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public string? UseFolder { get; set; }
    public bool DeleteFilesIfExists { get; set; }
}
