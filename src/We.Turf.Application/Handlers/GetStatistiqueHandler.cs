using Microsoft.EntityFrameworkCore;

namespace We.Turf.Handlers;

#region Normal Stat
file class StatistiqueByClassifier_ : Specification<Stat>
{
    public StatistiqueByClassifier_(string classifier) : base(e => e.Classifier == classifier) { }
}

file class StatistiqueByPari_ : Specification<Stat>
{
    public StatistiqueByPari_(string pari, bool includeNonArrivee)
        : base(
            e =>
                e.Pari == pari
                || (includeNonArrivee ? e.Pari == null : true)
        )
    { }
}

public class GetStatistiqueHandler
    : AbpHandler.With<GetStatistiqueQuery, GetStatistiqueResponse, Stat, StatDto>
{
    public GetStatistiqueHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }


    protected override async Task<Result<GetStatistiqueResponse>> InternalHandle(GetStatistiqueQuery request, CancellationToken cancellationToken)
    {
        var query = await Repository.GetQueryableAsync();
        if (request.Classifier is not null)
            query = query.GetQuery(new StatistiqueByClassifier_(request.Classifier));
        if (request.Pari != TypePari.Tous)
            query = query.GetQuery(
                new StatistiqueByPari_(request.Pari.AsString(), request.IncludeNonArrive)
            );
        var s = query.ToQueryString();
        LogDebug(s);
        var res = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new GetStatistiqueResponse(MapToDtoList(res));
    }
}

#endregion

#region Stats with Dat
file class StatistiqueByDate : Specification<StatByDate>
{
    public StatistiqueByDate(DateOnly date) : base(e => e.Date == date) { }
}

file class StatistiqueByInterval : Specification<StatByDate>
{
    public StatistiqueByInterval(DateOnly start_date, DateOnly end_date)
        : base(e => e.Date >= start_date && e.Date <= end_date) { }
}

file class StatistiqueByClassifier : Specification<StatByDate>
{
    public StatistiqueByClassifier(string classifier) : base(e => e.Classifier == classifier) { }
}

file class StatistiqueByPari : Specification<StatByDate>
{
    public StatistiqueByPari(string pari, bool includeNonArrivee)
        : base(
            e =>
                e.Pari == pari
                || (includeNonArrivee ? e.Pari == null : true)
        ) { }
}

public class GetStatistiqueWithDateHandler
    : AbpHandler.With<
          GetStatistiqueWithDateQuery,
          GetStatistiqueWithDateResponse,
          StatByDate,
          StatByDateDto
      >
{
    public GetStatistiqueWithDateHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }



    protected override async Task<Result<GetStatistiqueWithDateResponse>> InternalHandle(GetStatistiqueWithDateQuery request, CancellationToken cancellationToken)
    {
        var query = await Repository.GetQueryableAsync();
        if (request.Start is not null)
            if (request.End is not null)
                query = query.GetQuery(
                    new StatistiqueByInterval((DateOnly)request.Start, (DateOnly)request.End)
                );
            else
                query = query.GetQuery(new StatistiqueByDate((DateOnly)request.Start));

        if (request.Classifier != TurfDomainConstants.ALL_CLASSIFIER)
            query = query.GetQuery(new StatistiqueByClassifier(request.Classifier));

        if (request.Pari != TypePari.Tous)
            query = query.GetQuery(
                new StatistiqueByPari(request.Pari.AsString(), request.IncludeNonArrive)
            );
        var s = query.ToQueryString();
        LogDebug(s);
        var res = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new GetStatistiqueWithDateResponse(MapToDtoList(res));
    }
}
#endregion
