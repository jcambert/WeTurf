using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

internal class ResultatOfPredictedStatisticalByDate : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedStatisticalByDate(DateOnly date) : base(e => e.Date == date) { }
}

internal class ResultatOfPredictedStatisticalByClassifier : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedStatisticalByClassifier(string classifier)
        : base(e => e.Classifier == classifier) { }
}

internal class ResultatOfPredictedStatisticalByPari : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedStatisticalByPari(string pari) : base(e => e.Pari == pari) { }
}

internal class ResultatOfPredictedStatisticalByCourse : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedStatisticalByCourse(DateOnly date, int reunion, int course)
        : base(e => e.Date == date && e.Reunion == reunion && e.Course == course) { }
}

internal class ResultatOfPredictedStatisticalSpecification : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedStatisticalSpecification()
    {
        AddDistinct();
        AddOrderBy(x => x.Reunion);
        AddOrderBy(x => x.Course);
        AddOrderBy(x => x.NumeroPmu);
    }
}

public class BrowseResultatOfPredictedStatisticalHandler
    : AbpHandler.With<
          BrowseResultatOfPredictedStatisticalQuery,
          BrowseResultatOfPredictedStatisticalResponse,
          ResultatOfPredicted,
          ResultatOfPredictedStatisticalDto
      >
{
    public BrowseResultatOfPredictedStatisticalHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }
#if MEDIATOR
    public override async ValueTask<Result<BrowseResultatOfPredictedStatisticalResponse>> Handle(
        BrowseResultatOfPredictedStatisticalQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowseResultatOfPredictedStatisticalResponse>> Handle(
        BrowseResultatOfPredictedStatisticalQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        LogTrace($"{nameof(BrowseResultatOfPredictedStatisticalHandler)}");
        if (request.Date is null)
            return Result.Failure<BrowseResultatOfPredictedStatisticalResponse>(
                new Error("You must specify a valid Date")
            );

        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();

        if (request.Reunion is not null && request.Course is not null)
            query = query.GetQuery(
                new ResultatOfPredictedStatisticalByCourse(
                    date,
                    request.Reunion ?? 0,
                    request.Course ?? 0
                )
            );
        else
            query = query.GetQuery(new ResultatOfPredictedStatisticalByDate(date));

        if (!string.IsNullOrEmpty(request.Classifier))
            query = query.GetQuery(
                new ResultatOfPredictedStatisticalByClassifier(request.Classifier)
            );

        if (request.Pari != TypePari.Tous)
            query = query.GetQuery(
                new ResultatOfPredictedStatisticalByPari(request.Pari.AsString())
            );

        query = query.GetQuery(new ResultatOfPredictedStatisticalSpecification());
        /*query = query
            .Distinct()
            
            .OrderBy(x => x.Reunion)
            .ThenBy(x => x.Course)
            .ThenBy(x => x.NumeroPmu);*/
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatOfPredictedStatisticalResponse(MapToDtoList(result));
    }
}
