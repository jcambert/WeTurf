namespace We.Turf.Handlers;

public class BrowseDisciplineHandler
    : AbpHandler.With<BrowseDisciplineQuery, BrowseDisciplineResponse, Predicted, DisciplineDto>
{
    public BrowseDisciplineHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

    protected override async Task<Result<BrowseDisciplineResponse>> InternalHandle(
        BrowseDisciplineQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await Repository.GetQueryableAsync();
        //query = query.DistinctBy(x => x.Specialite).OrderBy(x => x.Specialite);

        var res = await AsyncExecuter.ToListAsync(query, cancellationToken);

        var res0 = res.Select(x => new DisciplineDto() { Nom = x.Specialite ?? string.Empty })
            .DistinctBy(x => x.Nom)
            .OrderBy(x => x.Nom);
        return new BrowseDisciplineResponse(res0.ToList());
    }
}
