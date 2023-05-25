using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class ResultatOfPredictedCountByReunionCourseHandler
    : AbpHandler.With<
          ResultatOfPredictedCountByReunionCourseQuery,
          ResultatOfPredictedCountByReunionCourseResponse,
          ResultatOfPredicted,
          ResultatOfPredictedCountByReunionCourseDto
      >
{
    public ResultatOfPredictedCountByReunionCourseHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }
#if MEDIATOR
    public override async ValueTask<Result<ResultatOfPredictedCountByReunionCourseResponse>> Handle(
        ResultatOfPredictedCountByReunionCourseQuery request,
        CancellationToken cancellationToken
    )
#else
    public override async Task<Result<ResultatOfPredictedCountByReunionCourseResponse>> Handle(
        ResultatOfPredictedCountByReunionCourseQuery request,
        CancellationToken cancellationToken
    )
#endif
    {
        var query0 = await Repository.GetQueryableAsync();

        var query1 =
            from q in query0
            where
                q.Date == request.Date
                && q.Classifier == request.Classifier
                && (q.Pari == request.Pari.AsString() || q.Pari == null)
            orderby q.Date ,q.Classifier ,q.Reunion ,q.Course
            group q by new { q.Date, q.Classifier, q.Reunion, q.Course } into qq
            select new ResultatOfPredictedCountByReunionCourse(
                qq.Key.Date,
                qq.Key.Classifier,
                qq.Key.Reunion,
                qq.Key.Course,
                qq.Count()
            );

        LogDebug(query1.ToQueryString());
        var res = await AsyncExecuter.ToListAsync(query1, cancellationToken);

        return new ResultatOfPredictedCountByReunionCourseResponse(
            ObjectMapper.Map<
                List<ResultatOfPredictedCountByReunionCourse>,
                List<ResultatOfPredictedCountByReunionCourseDto>
            >(res)
        );
    }
}
