using We.Mediatr;
using We.Turf.Entities;

namespace We.Turf.Queries;

public interface ILoadCourseIntoDbQuery : IQuery<LoadCourseIntoDbResponse>
{
    string Filename { get; set; }
    bool Rename { get; set; }
}

public sealed record LoadCourseIntoDbResponse(List<CourseDto> Courses) : Response;
