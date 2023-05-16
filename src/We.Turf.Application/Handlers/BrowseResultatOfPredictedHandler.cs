using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

internal class ResultatOfPredictedByDate : Specification<ResultatOfPredicted>
{
    public ResultatOfPredictedByDate(DateOnly date) : base(e => e.Date == date) { }
}

public class BrowseResultatOfPredictedHandler
    : AbpHandler.With<
          BrowseResultatOfPredictedQuery,
          BrowseResultatOfPredictedResponse,
          ResultatOfPredicted,
          ResultatOfPredictedDto,
          Guid
      >
{
    public BrowseResultatOfPredictedHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }
#if MEDIATOR
    public override async ValueTask<Result<BrowseResultatOfPredictedResponse>> Handle(
        BrowseResultatOfPredictedQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<BrowseResultatOfPredictedResponse>> Handle(
        BrowseResultatOfPredictedQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new ResultatOfPredictedByDate(date));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        return new BrowseResultatOfPredictedResponse(MapToDtoList(result));
    }
}
