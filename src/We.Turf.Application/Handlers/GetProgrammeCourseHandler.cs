using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class GetProgrammeCourseHandler
    : AbpHandler.With<
          GetProgrammeCourseQuery,
          GetProgrammeCourseResponse,
          ProgrammeCourse,
          ProgrammeCourseDto,
          Guid
      >
{
    public GetProgrammeCourseHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

#if MEDIATOR
    public override async ValueTask<Result<GetProgrammeCourseResponse>> Handle(
        GetProgrammeCourseQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<GetProgrammeCourseResponse>> Handle(
        GetProgrammeCourseQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new ProgrammeCourseById(request.Id));
        var result = await AsyncExecuter.FirstOrDefaultAsync(query, cancellationToken);
        if (result == null)
        {
            return NotFound($"{nameof(ProgrammeCourse)} with Id:{request.Id} didn't exists");
        }
        return new GetProgrammeCourseResponse(MapToDto(result));
    }
}
