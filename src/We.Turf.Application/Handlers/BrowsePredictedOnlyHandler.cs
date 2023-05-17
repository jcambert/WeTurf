using AutoMapper.QueryableExtensions;
using System.Security.Cryptography;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;
using We.Utilities;

namespace We.Turf.Handlers;

internal class PredictedOnlyByDate : Specification<Predicted>
{
    public PredictedOnlyByDate(DateOnly date) : base(e => e.Date == date) { }
}

internal class PredictedOnlyByClassifier : Specification<Predicted>
{
    public PredictedOnlyByClassifier(string classifier) : base(e => e.Classifier == classifier) { }
}

internal class PredictedOnlySpecification : Specification<Predicted>
{
    public PredictedOnlySpecification()
    {
        AddDistinct();
        AddOrderBy(x => x.Reunion);
        AddOrderBy(x => x.Course);
        AddOrderBy(x => x.NumeroPmu);
    }
}

public class BrowsePredictedOnlyHandler
    : AbpHandler.With<
          BrowsePredictedOnlyQuery,
          BrowsePredictedOnlyResponse,
          Predicted,
          PredictedOnlyDto
      >
{
    public BrowsePredictedOnlyHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

#if MEDIATOR
    public override async ValueTask<Result<BrowsePredictedOnlyResponse>> Handle(
        BrowsePredictedOnlyQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async ValueTask<Result<BrowsePredictedOnlyResponse>> Handle(
        BrowsePredictedOnlyQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        LogTrace($"{nameof(BrowsePredictedOnlyHandler)}");

        if (request.Date is null)
            return Result.Failure<BrowsePredictedOnlyResponse>(
                new Error("You must specify a date")
            );

        var date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);

        var query = await Repository.GetQueryableAsync();
        query = query.GetQuery(new PredictedOnlyByDate(date));

        if (!string.IsNullOrEmpty(request.Classifier))
            query = query.GetQuery(new PredictedOnlyByClassifier(request.Classifier));

        /*
        query = query
            .Distinct()
            .OrderBy(x => x.Reunion)
            .ThenBy(x => x.Course)
            .ThenBy(x => x.NumeroPmu);*/

        query = query.GetQuery(new PredictedOnlySpecification());

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);
        var res = MapToDtoList(result);
        // res.ForEach(x => x.Id = Guid.Empty);
        res = res.DistinctBy(x => x.Hash).ToList();
        return new BrowsePredictedOnlyResponse(res);
    }
}
