using Microsoft.Extensions.Logging;
using System.Reactive.Linq;
using We.Csv;
using We.Utilities;

namespace We.Turf.Handlers;

public class LoadToPredictIntoDbHandler
    : AbpHandler.With<
          LoadToPredictIntoDbQuery,
          LoadToPredictIntoDatabaseResponse,
          ToPredict,
          ToPredictDto,
          Guid
      >
{
    public LoadToPredictIntoDbHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider) { }

    protected override async Task<Result<LoadToPredictIntoDatabaseResponse>> InternalHandle(
        LoadToPredictIntoDbQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (File.Exists(request.Filename))
            {
                var query = await Repository.GetQueryableAsync();
                var query1 = query.Select(x => new { x.Date, x.Reunion, x.Course }).Distinct();
                var existings = await AsyncExecuter.ToListAsync(query1, cancellationToken);

                var reader = new Reader<ToPredict>($"{request.Filename}", true, ';');
                List<ToPredict> courses = new();
                _ = reader.OnReadLine
                    .Where(
                        x =>
                            !existings.Any(
                                y =>
                                    y.Date == x.Value.Date
                                    && y.Reunion == x.Value.Reunion
                                    && y.Course == x.Value.Course
                            )
                    )
                    .Subscribe(
                        o =>
                        {
                            Logger.LogInformation("{Index} / {Response}", o.Index, o);
                            courses.Add(o.Value);
                        },
                        ex =>
                        {
                            Logger.LogWarning("{Message}", ex.Message);
                        },
                        () =>
                        {
                            if (request.Rename)
                                File.Move(
                                    request.Filename,
                                    request.Filename.GenerateCopyName(null),
                                    true
                                );
                        }
                    );

                var result = await reader.Start(cancellationToken);

                await Repository.InsertManyAsync(courses, true, cancellationToken);
                if (result.Errors.Any())
                    return Result.ValidWithFailure(
                        new LoadToPredictIntoDatabaseResponse(MapToDtoList(courses)),
                        result.Errors.ToArray()
                    );

                return new LoadToPredictIntoDatabaseResponse(MapToDtoList(courses));
            }
            return Result.Failure<LoadToPredictIntoDatabaseResponse>(
                new Error($"{request.Filename} n'existe pas")
            );
        }
        catch (Exception ex)
        {
            return Result.Failure<LoadToPredictIntoDatabaseResponse>(ex);
        }
    }
}
