namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IResultatQuery))]
public class ResultatQuery : IResultatQuery
{
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public string? UseFolder { get; set; }
    public bool DeleteFilesIfExists { get; set; }
}
