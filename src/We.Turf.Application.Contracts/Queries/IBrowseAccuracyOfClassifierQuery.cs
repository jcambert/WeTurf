using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseAccuracyOfClassifierQuery
    : WeM.IQuery<BrowseAccuracyOfClassifierResponse> { }

public sealed record BrowseAccuracyOfClassifierResponse(
    List<AccuracyPerClassifierDto> ClassifiersAccuracy
) : WeM.Response;
