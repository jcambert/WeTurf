namespace We.Turf.Handlers;

file class BrowseProgrammeReunionByDateSpec : Specification<ProgrammeReunion>
{
    public BrowseProgrammeReunionByDateSpec(DateOnly date) : base(e => e.Date == date) { }
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


    protected override async Task<Result<BrowseProgrammeReunionResponse>> InternalHandle(BrowseProgrammeReunionQuery request, CancellationToken cancellationToken)
    {
        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new BrowseProgrammeReunionByDateSpec(request.Date));
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseProgrammeReunionResponse(MapToDtoList(result));
    }
}
