using We.Turf.Entities;

namespace We.Turf.Queries;

public enum TypePari
{
    Place,
    Gagnant,
    NonArrive,
    Tous
}

public static class TypePariExtensions
{
    public static string AsString(this TypePari pari) =>
        pari switch
        {
            TypePari.Place => "E_SIMPLE_PLACE",
            TypePari.Gagnant => "E_SIMPLE_GAGNANT",
            TypePari.NonArrive => "E_NON_ARRIVE",
            _ => ""
        };
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
