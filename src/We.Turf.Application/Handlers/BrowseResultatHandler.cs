namespace We.Turf.Handlers;

file class ResultatByDateSpec : Specification<Resultat>
{
    public ResultatByDateSpec(DateOnly date) : base(e => e.Date == date) { }
}

public class BrowseResultatHandler
    : AbpHandler.With<BrowseResultatQuery, BrowseResultatResponse, Resultat, ResultatDto>
{
    public BrowseResultatHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }


    protected override async Task<Result<BrowseResultatResponse>> InternalHandle(BrowseResultatQuery request, CancellationToken cancellationToken)
    {
        LogTrace($"{nameof(BrowseResultatHandler)}");
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new ResultatByDateSpec(date));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatResponse(MapToDtoList(result));
    }
}
