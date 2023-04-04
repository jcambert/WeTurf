using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;
internal class ProgrammeCourseByDate : Specification<ProgrammeCourse>
{
    public ProgrammeCourseByDate(DateOnly date) : base(e => e.Date == date)
    {
    }
}
public class BrowseProgrammeCourseHandler : AbpHandler.With<BrowseProgrammeCourseQuery, BrowseProgrammeCourseResponse>
{
    IRepository<ProgrammeCourse, Guid> repository => GetRequiredService<IRepository<ProgrammeCourse, Guid>>();

    public BrowseProgrammeCourseHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<BrowseProgrammeCourseResponse>> Handle(BrowseProgrammeCourseQuery request, CancellationToken cancellationToken)
    {
        var query = await repository.GetQueryableAsync();
        query = query.GetQuery(new ProgrammeCourseByDate(request.Date));
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseProgrammeCourseResponse(ObjectMapper.Map<List<ProgrammeCourse>, List<ProgrammeCourseDto>>(result));
    }
}
