using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatPerClassifierQuery
    : WeM.IQuery<BrowseResultatPerClassifierResponse> { }

public sealed record BrowseResultatPerClassifierResponse(
    List<ResultatPerClassifierDto> ResultatPerClassifiers
) : WeM.Response;
