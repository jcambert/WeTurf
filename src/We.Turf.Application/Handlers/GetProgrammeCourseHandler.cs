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

    protected override async Task<Result<GetProgrammeCourseResponse>> InternalHandle(
        GetProgrammeCourseQuery request,
        CancellationToken cancellationToken
    )
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
