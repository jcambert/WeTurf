using Microsoft.EntityFrameworkCore;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

file class GetMiseByDateSpec : Specification<Predicted>
{
    public GetMiseByDateSpec(DateOnly date) : base(e => e.Date == date)
    {
        this.AddDistinct();
        this.AddOrderBy(e => e.Reunion);
        this.AddOrderBy(e => e.Course);
        this.AddGroupBy(e => new { e.Date, e.Reunion, e.Course });


    }
}

file class GetMiseByReunionCourseSpec : Specification<Predicted>
{
    public GetMiseByReunionCourseSpec(DateOnly date, int Reunion, int course) : base(e => e.Date == date && e.Reunion == Reunion && e.Course == course)
    {
        this.AddDistinct();
        this.AddOrderBy(e => e.Reunion);
        this.AddOrderBy(e => e.Course);
        this.AddGroupBy(e => new { e.Date, e.Reunion, e.Course });
    }
}

file class GetMiseByClassifierSpec : Specification<Predicted>
{
    public GetMiseByClassifierSpec(string classifier) : base(e => e.Classifier == classifier)
    {

    }
}
public class GetMiseHandler : AbpHandler.With<GetMiseQuery, GetMiseResponse, Predicted, MiseDto>
{

    public GetMiseHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private static Specification<Predicted> GetSpecification(GetMiseQuery request)
        => request switch
        {
            { Reunion: { }, Course: { } } => new GetMiseByReunionCourseSpec(request.Date, (int)request.Reunion, (int)request.Course),
            _ => new GetMiseByDateSpec(request.Date)
        };


#if MEDIATOR
    public override async ValueTask<Result<GetMiseResponse>> Handle(GetMiseQuery request, CancellationToken cancellationToken)
#else
    public override async Task<Result<GetMiseResponse>> Handle(GetMiseQuery request, CancellationToken cancellationToken)
#endif
    {
        var query = await Repository.GetQueryableAsync();
        //query = query.GetQuery( GetSpecification(request));
        IQueryable<MiseDto> q0 = q0 = from q in query
                                      orderby q.Date, q.Classifier, q.Reunion, q.Course
                                      group q by new { q.Date, q.Classifier, q.Reunion, q.Course } into qq
                                      select new MiseDto() { Date = qq.Key.Date, Classifier = qq.Key.Classifier, Reunion = qq.Key.Reunion, Course = qq.Key.Course, Somme = qq.Count() };
        q0 = q0.Where(x => x.Date == request.Date);
        if (request.Reunion is not null && request.Course is not null)
            q0 = q0.Where(x =>  x.Reunion == request.Reunion && x.Course == request.Course);
        ;
        if (!string.IsNullOrEmpty(request.Classifier))
            q0 = q0.Where(x => x.Classifier == request.Classifier);
        var sql = q0.ToQueryString();
        LogDebug(sql);
        var res = await AsyncExecuter.ToListAsync(q0, cancellationToken);
        return new GetMiseResponse(res);

    }
}
