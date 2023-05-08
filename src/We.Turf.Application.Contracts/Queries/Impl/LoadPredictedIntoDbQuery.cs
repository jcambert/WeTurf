namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ILoadPredictedIntoDbQuery))]
public class LoadPredictedIntoDbQuery : ILoadPredictedIntoDbQuery
{
    public string Filename { get; set; }
    public bool Rename { get; set; } = true;

    public bool HasHeader { get; set; } = true;

    public char Separator { get; set; } = ';';
}
