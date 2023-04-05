using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class GetProgrammeCourseHandler : AbpHandler.With<GetProgrammeCourseQuery, GetProgrammeCourseResponse>
{
    IRepository<ProgrammeCourse, Guid> repository => GetRequiredService<IRepository<ProgrammeCourse, Guid>>();
    public GetProgrammeCourseHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<GetProgrammeCourseResponse>> Handle(GetProgrammeCourseQuery request, CancellationToken cancellationToken)
    {
        var query = await repository.GetQueryableAsync();
        query = query.GetQuery(new ProgrammeCourseById(request.Id));
        var result = await AsyncExecuter.FirstOrDefaultAsync(query, cancellationToken);
        if (result == null)
        {
            return NotFound($"{nameof(ProgrammeCourse)} with Id:{request.Id} didn't exists");
        }
        return new GetProgrammeCourseResponse(ObjectMapper.Map<ProgrammeCourse,ProgrammeCourseDto>(result));
    }
}
