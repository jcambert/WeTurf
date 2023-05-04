using Volo.Abp.Domain.Repositories;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

internal class BrowseProgrammeReunionByDate : Specification<ProgrammeReunion>
{
    public BrowseProgrammeReunionByDate(DateOnly date) : base(e => e.Date == date) { }
}

public class BrowseProgrammeReunionHandler
    : AbpHandler.With<
          BrowseProgrammeReunionQuery,
          BrowseProgrammeReunionResponse,
          ProgrammeReunion,
          ProgrammeReunionDto
      >
{
    public BrowseProgrammeReunionHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

#if MEDIATOR
    public override async ValueTask<Result<BrowseProgrammeReunionResponse>> Handle(
        BrowseProgrammeReunionQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowseProgrammeReunionResponse>> Handle(
        BrowseProgrammeReunionQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new BrowseProgrammeReunionByDate(request.Date));
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseProgrammeReunionResponse(MapToDtoList(result));
    }
}
