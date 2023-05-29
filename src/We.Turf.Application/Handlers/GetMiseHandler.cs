using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace We.Turf.Handlers;

[Dependency(ServiceLifetime.Scoped, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<GetMiseQuery>))]
public class GetMiseQueryValidator : AbstractValidator<GetMiseQuery>
{
    public GetMiseQueryValidator()
    {
        this.RuleFor(x => x.Date).GreaterThanOrEqualTo(TurfDomainConstants.MIN_DATE);
        this.RuleFor(x => x.Reunion).NotNull().When(x => x.Course != null);
        this.RuleFor(x => x.Course).NotNull().When(x => x.Reunion != null);
    }
}

public class GetMiseHandler : AbpHandler.With<GetMiseQuery, GetMiseResponse, Predicted, MiseDto>
{
    public GetMiseHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<GetMiseResponse>> InternalHandle(
        GetMiseQuery request,
        CancellationToken cancellationToken
    )
    {
        List<MiseDto> result = new();
        var query = await Repository.GetQueryableAsync();
        if (!request.Classifier.IsAllClassifier())
        {
            IQueryable<MiseDto> q0 =
                from q in query
                where q.Date == request.Date && q.Classifier == request.Classifier
                orderby q.Date ,q.Classifier ,q.Reunion ,q.Course
                group q by new { q.Date, q.Classifier, q.Reunion, q.Course } into qq
                select new MiseDto()
                {
                    Date = qq.Key.Date,
                    Classifier = qq.Key.Classifier,
                    Reunion = qq.Key.Reunion,
                    Course = qq.Key.Course,
                    Somme = qq.Count()
                };

            if (request.Reunion is not null && request.Course is not null)
                q0 = q0.Where(x => x.Reunion == request.Reunion && x.Course == request.Course);

            var sql = q0.ToQueryString();
            LogDebug(sql);
            result = await AsyncExecuter.ToListAsync(q0, cancellationToken);
        }
        else
        {
            var q0 = (
                from q in query
                where q.Date == request.Date
                orderby q.Date ,q.NumeroPmu ,q.Reunion ,q.Course
                select new
                {
                    Date = q.Date,
                    NumeroPmu = q.NumeroPmu,
                    Reunion = q.Reunion,
                    Course = q.Course
                }
            ).Distinct();
            if (request.Reunion is not null && request.Course is not null)
                q0 = q0.Where(x => x.Reunion == request.Reunion && x.Course == request.Course);
            var sql = q0.ToQueryString();
            LogDebug(sql);
            var res = await AsyncExecuter.ToListAsync(q0, cancellationToken);
            result = res.Select(
                    x =>
                        new MiseDto
                        {
                            Date = x.Date,
                            Reunion = x.Reunion,
                            Course = x.Course,
                            Somme = 1
                        }
                )
                .ToList();
        }

        return new GetMiseResponse(result);
    }
}
