using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IBrowseProgrammeCourseQuery : IQuery<BrowseProgrammeCourseResponse>
{
    DateOnly Date { get; set; }
}

public sealed record BrowseProgrammeCourseResponse(List<ProgrammeCourseDto> Programmes) : Response;
