namespace We.Turf.Handlers;


file class ByDateSpec : Specification<ResultatOfPredictedWithoutClassifier>
{
    public ByDateSpec(DateOnly date) : base(e => e.Date == date) { }
}
file class ByCourseSpec : Specification<ResultatOfPredictedWithoutClassifier>
{
    public ByCourseSpec(DateOnly date,int reunion,int course) : base(e => e.Date == date && e.Reunion==reunion && e.Course==course) { }
}
file class ByPariSpec : Specification<ResultatOfPredictedWithoutClassifier>
{
    public ByPariSpec(string pari,bool includeNonArrivee) : base(e => e.Pari==pari || (includeNonArrivee ? e.Pari == null : true)) { }
}


public class BrowseResultatOfPredictedWithoutClassifierHandler : AbpHandler.With<BrowseResultatOfPredictedWithoutClassifierQuery, BrowseResultatOfPredictedWithoutClassifierResponse, ResultatOfPredictedWithoutClassifier, ResultatOfPredictedWithoutClassifierDto>
{
    public BrowseResultatOfPredictedWithoutClassifierHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }


    protected override async Task<Result<BrowseResultatOfPredictedWithoutClassifierResponse>> InternalHandle(BrowseResultatOfPredictedWithoutClassifierQuery request, CancellationToken cancellationToken)
    {
        LogTrace($"{nameof(BrowseResultatOfPredictedWithoutClassifierResponse)}");


        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();

        if (request.Reunion is not null && request.Course is not null)
            query = query.GetQuery(
                new ByCourseSpec(
                    date,
                    request.Reunion ?? 0,
                    request.Course ?? 0
                )
            );
        else
            query = query.GetQuery(new ByDateSpec(date));



        if (request.Pari != TypePari.Tous)
            query = query.GetQuery(
                new ByPariSpec(request.Pari.AsString(), request.IncludeNonArrive)
            );


        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatOfPredictedWithoutClassifierResponse(MapToDtoList(result));
    }
}
