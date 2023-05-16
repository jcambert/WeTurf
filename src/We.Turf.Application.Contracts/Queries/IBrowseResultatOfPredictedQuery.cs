using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatOfPredictedQuery : WeM.IQuery<BrowseResultatOfPredictedResponse>
{
    DateOnly? Date { get; set; }
}

public sealed record BrowseResultatOfPredictedResponse(List<ResultatOfPredictedDto> Resultats)
    : WeM.Response;
