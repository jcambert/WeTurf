using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetProgrammeCourseQuery : IQuery<GetProgrammeCourseResponse>
{
    Guid Id { get; set; }
}

public sealed record GetProgrammeCourseResponse(ProgrammeCourseDto Course) : Response;
