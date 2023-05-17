using We.Turf.Entities;

namespace We.Turf.Queries;

public enum TypePari
{
    Simple,
    Gagnant,
    Tous
}

public interface IBrowseResultatOfPredictedStatisticalQuery
    : WeM.IQuery<BrowseResultatOfPredictedStatisticalResponse>
{
    DateOnly? Date { get; set; }
    string? Classifier { get; set; }

    TypePari Pari { get; set; }
}

public sealed record BrowseResultatOfPredictedStatisticalResponse(
    List<ResultatOfPredictedStatisticalDto> Resultats
) : WeM.Response;
