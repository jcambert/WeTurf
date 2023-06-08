using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseDisciplineQuery : WeM.IQuery<BrowseDisciplineResponse> { }

public sealed record BrowseDisciplineResponse(List<DisciplineDto> Discipline) : WeM.Response;
