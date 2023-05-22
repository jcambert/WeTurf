using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatOfPredictedWithoutClassifierQuery
    : WeM.IQuery<BrowseResultatOfPredictedWithoutClassifierResponse>
{
    DateOnly? Date { get; set; }
    TypePari Pari { get; set; }

    int? Reunion { get; set; }
    int? Course { get; set; }
    bool IncludeNonArrive { get; set; }
}

public sealed record BrowseResultatOfPredictedWithoutClassifierResponse(
    List<ResultatOfPredictedWithoutClassifierDto> Resultats
) : WeM.Response;
