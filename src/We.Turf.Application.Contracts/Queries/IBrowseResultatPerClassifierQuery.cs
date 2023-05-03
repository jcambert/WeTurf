using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseResultatPerClassifierQuery : IQuery<BrowseResultatPerClassifierResponse> { }

public sealed record BrowseResultatPerClassifierResponse(
    List<ResultatPerClassifierDto> ResultatPerClassifiers
) : Response;
