using We.Turf.Entities;

namespace We.Turf.Queries;

public interface IGetProgrammeCourseQuery : WeM.IQuery<GetProgrammeCourseResponse>
{
    Guid Id { get; set; }
}

public sealed record GetProgrammeCourseResponse(ProgrammeCourseDto Course) : WeM.Response;
