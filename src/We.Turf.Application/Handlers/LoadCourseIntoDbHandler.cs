using Microsoft.Extensions.Logging;
using System.IO;
using System.Reactive.Linq;
using We.AbpExtensions;
using We.Csv;
using We.Results;
using We.Turf.Entities;
using We.Utilities;

namespace We.Turf.Handlers;

public class LoadCourseIntoDbHandler : AbpHandler.With<LoadCourseIntoDbQuery, LoadCourseIntoDbResponse>
{
    IRepository<Course, Guid> _repository => GetRequiredService<IRepository<Course, Guid>>();
    public LoadCourseIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<LoadCourseIntoDbResponse>> Handle(LoadCourseIntoDbQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(request.Filename))
            {

                var query0 = await _repository.GetQueryableAsync();
                var query1 = query0.Select(x => new { x.Date, x.Reunion, x.Numero }).Distinct();
                var existings = await AsyncExecuter.ToListAsync(query1, cancellationToken);

                var reader = new Reader<Course>($"{request.Filename}", true, ';');
                List<Course> courses = new List<Course>();
                reader
                    .OnReadLine
                    .Where(x => !existings.Any(y => y.Date == x.Value.Date && y.Reunion == x.Value.Reunion && y.Numero == x.Value.Numero))
                    .Subscribe(o =>
                    {
                        Logger.LogInformation($"{o.Index} / {o.ToString()}");
                        courses.Add(o.Value);
                    },
                        () =>
                        {
                            if (request.Rename)
                                File.Move(request.Filename, request.Filename.GenerateCopyName(null), true);
                        });

                await reader.Start(cancellationToken);

                await _repository.InsertManyAsync(courses, true, cancellationToken);

                return new LoadCourseIntoDbResponse(ObjectMapper.Map<List<Course>, List<CourseDto>>(courses));
            }
            return Result.Failure<LoadCourseIntoDbResponse>(new Error($"{request.Filename} n'existe pas"));
        }
        catch (Exception ex)
        {
            return Result.Failure<LoadCourseIntoDbResponse>(ex);
        }
    }
}
