using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class GetStatistiqueHandler
    : AbpHandler.With<GetStatistiqueQuery, GetStatistiqueResponse, Stat, StatDto>
{
    public GetStatistiqueHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

#if MEDIATOR
    public override async ValueTask<Result<GetStatistiqueResponse>> Handle(
        GetStatistiqueQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<GetStatistiqueResponse>> Handle(
        GetStatistiqueQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var res = await Repository.ToListAsync(cancellationToken);
        return new GetStatistiqueResponse(MapToDtoList(res));
    }
}

internal class StatistiqueByDate : Specification<StatByDate>
{
    public StatistiqueByDate(DateOnly date) : base(e => e.Date == date) { }
}

internal class StatistiqueByInterval : Specification<StatByDate>
{
    public StatistiqueByInterval(DateOnly start_date, DateOnly end_date)
        : base(e => e.Date >= start_date && e.Date <= end_date) { }
}

internal class StatistiqueByClassifier : Specification<StatByDate>
{
    public StatistiqueByClassifier(string classifier) : base(e => e.Classifier == classifier) { }
}

internal class StatistiqueByPari : Specification<StatByDate>
{
    public StatistiqueByPari(string pari) : base(e => e.Pari == pari) { }
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

#if MEDIATOR
    public override async ValueTask<Result<GetStatistiqueWithDateResponse>> Handle(
        GetStatistiqueWithDateQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<GetStatistiqueWithDateResponse>> Handle(
        GetStatistiqueWithDateQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var query = await Repository.GetQueryableAsync();
        if (request.Start is not null)
            if (request.End is not null)
                query = query.GetQuery(
                    new StatistiqueByInterval((DateOnly)request.Start, (DateOnly)request.End)
                );
            else
                query = query.GetQuery(new StatistiqueByDate((DateOnly)request.Start));

        if (!string.IsNullOrEmpty(request.Classifier))
            query = query.GetQuery(new StatistiqueByClassifier(request.Classifier));

        if (request.Pari != TypePari.Tous)
            query = query.GetQuery(new StatistiqueByPari(request.Pari.AsString()));

        var res = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new GetStatistiqueWithDateResponse(MapToDtoList(res));
    }
}
