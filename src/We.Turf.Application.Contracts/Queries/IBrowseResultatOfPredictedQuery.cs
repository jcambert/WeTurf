using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatOfPredictedQuery : WeM.IQuery<BrowseResultatOfPredictedResponse> { }

public sealed record BrowseResultatOfPredictedResponse(List<ResultatOfPredictedDto> Resultats)
    : WeM.Response;
