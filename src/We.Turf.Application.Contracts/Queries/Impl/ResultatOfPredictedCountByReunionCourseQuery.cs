namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IResultatOfPredictedCountByReunionCourseQuery))]
public class ResultatOfPredictedCountByReunionCourseQuery
    : IResultatOfPredictedCountByReunionCourseQuery
{
    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public TypePari Pari { get; set; } = TypePari.Place;
}
