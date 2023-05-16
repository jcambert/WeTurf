using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

internal class ResultatOfPredictedStatisticalByDate : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedStatisticalByDate(DateOnly date) : base(e => e.Date == date) { }
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
        query = query.GetQuery(new ResultatOfPredictedStatisticalByDate(date));
        query = query
            .Distinct()
            .Where(x => x.Pari == "E_SIMPLE_PLACE")
            .OrderBy(x => x.Reunion)
            .ThenBy(x => x.Course)
            .ThenBy(x => x.NumeroPmu);
        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatOfPredictedStatisticalResponse(MapToDtoList(result));
    }
}
