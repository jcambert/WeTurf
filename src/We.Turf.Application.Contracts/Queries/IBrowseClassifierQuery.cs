using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseClassifierQuery : WeM.IQuery<BrowseClassifierResponse> { }

public sealed record BrowseClassifierResponse(List<ClassifierDto> Classifiers) : WeM.Response;
