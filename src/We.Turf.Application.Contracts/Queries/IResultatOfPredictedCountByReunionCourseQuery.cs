using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IResultatOfPredictedCountByReunionCourseQuery
    : WeM.IQuery<ResultatOfPredictedCountByReunionCourseResponse>
{
    DateOnly Date { get; set; }
    string Classifier { get; set; }

    TypePari Pari { get; set; }
}

public sealed record ResultatOfPredictedCountByReunionCourseResponse(
    List<ResultatOfPredictedCountByReunionCourseDto> Resultats
) : WeM.Response;
