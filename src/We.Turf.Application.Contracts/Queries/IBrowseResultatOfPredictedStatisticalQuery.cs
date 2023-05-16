using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatOfPredictedStatisticalQuery
    : WeM.IQuery<BrowseResultatOfPredictedStatisticalResponse>
{
    DateOnly? Date { get; set; }
}

public sealed record BrowseResultatOfPredictedStatisticalResponse(
    List<ResultatOfPredictedStatisticalDto> Resultats
) : WeM.Response;
