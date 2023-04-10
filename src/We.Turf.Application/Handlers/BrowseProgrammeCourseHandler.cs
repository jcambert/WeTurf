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
internal class ProgrammeCourseById : Specification<ProgrammeCourse>
{
    public ProgrammeCourseById(Guid id) : base(e => e.Id == id)
    {
    }
}
public class BrowseProgrammeCourseHandler : AbpHandler.With<BrowseProgrammeCourseQuery, BrowseProgrammeCourseResponse, ProgrammeCourse, ProgrammeCourseDto,Guid>
{
    

    public BrowseProgrammeCourseHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<BrowseProgrammeCourseResponse>> Handle(BrowseProgrammeCourseQuery request, CancellationToken cancellationToken)
    {
        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new ProgrammeCourseByDate(request.Date));
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseProgrammeCourseResponse(MapToDtoList(result));
    }
}
