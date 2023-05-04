using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseProgrammeCourseQuery : WeM.IQuery<BrowseProgrammeCourseResponse>
{
    DateOnly Date { get; set; }
}

public sealed record BrowseProgrammeCourseResponse(List<ProgrammeCourseDto> Programmes)
    : WeM.Response;
