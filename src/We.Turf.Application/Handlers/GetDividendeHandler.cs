namespace We.Turf.Handlers;

file class GetDividendeByDateSpec : Specification<ResultatOfPredicted>
{
    public GetDividendeByDateSpec(DateOnly date) : base(e => e.Date == date)
    {
        this.AddOrderBy(e => e.Date);
        this.AddOrderBy(e => e.Reunion);
        this.AddOrderBy(e => e.Course);
    }
}
file class GetDividendeByReunionCourseSpec : Specification<ResultatOfPredicted>
{
    public GetDividendeByReunionCourseSpec(DateOnly date, int reunion, int course) : base(e => e.Date == date && e.Reunion == reunion && e.Course == course)
    {
        this.AddOrderBy(e => e.Date);
        this.AddOrderBy(e => e.Reunion);
        this.AddOrderBy(e => e.Course);
    }
}

public class GetDividendeHandler : AbpHandler.With<GetDividendeQuery, GetDividendeResponse, ResultatOfPredicted, DividendeDto>
{
    public GetDividendeHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }


    protected override async Task<Result<GetDividendeResponse>> InternalHandle(GetDividendeQuery request, CancellationToken cancellationToken)
    {
        var query = await Repository.GetQueryableAsync();
        if (request.Reunion is not null && request.Course is not null)
            query = query.GetQuery(new GetDividendeByReunionCourseSpec(request.Date, (int)request.Reunion, (int)request.Course));
        else
            query = query.GetQuery(new GetDividendeByDateSpec(request.Date));

        bool hasClassifier = request.Classifier.IsAllClassifier();
        if (!hasClassifier)
            query = query.Where(x => x.Classifier == request.Classifier);

        if (request.Pari != TypePari.Tous)
            query = query.Where(x => x.Pari == request.Pari.AsString());

        var res = await AsyncExecuter.ToListAsync(query, cancellationToken);

        IEnumerable<DividendeDto> res0;

        res0 = res
        .GroupBy(x => new { x.Date, x.Reunion, x.Course })
        .Select(x => new DividendeDto() { Date = x.Key.Date, Reunion = x.Key.Reunion, Course = x.Key.Course, Somme = x.Sum(y => y.Dividende ?? 0.0), Classifier = (hasClassifier ? request.Classifier : string.Empty) });

        return Result.Create<GetDividendeResponse>(new(res0.ToList()));
    }
}
