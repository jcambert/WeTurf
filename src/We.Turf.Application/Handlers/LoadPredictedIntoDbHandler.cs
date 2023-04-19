using Microsoft.Extensions.Logging;
using System.IO;
using System.Reactive.Linq;
using We.AbpExtensions;
using We.Csv;
using We.Results;
using We.Turf.Entities;
using We.Utilities;

namespace We.Turf.Handlers;



public class LoadPredictedIntoDbHandler : AbpHandler.With<LoadPredictedIntoDbQuery, LoadPredictedIntoDbResponse, Predicted, PredictedDto, Guid>
{
   
    public LoadPredictedIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<LoadPredictedIntoDbResponse>> Handle(LoadPredictedIntoDbQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(request.Filename))
            {

                var query0 = await Repository.GetQueryableAsync();
                var query1 = query0.Select(x => new { x.Date, x.Reunion, x.Course }).Distinct();
                var existings = await AsyncExecuter.ToListAsync(query1, cancellationToken);

                var reader = new Reader<Predicted>($"{request.Filename}", true, ';');
                List<Predicted> predicted = new List<Predicted>();
                reader
                    .OnReadLine
                    .Where(x => !existings.Any(y => y.Date == x.Value.Date && y.Reunion == x.Value.Reunion && y.Course == x.Value.Course))
                    .Subscribe(o =>
                        {
                            Logger.LogInformation($"{o.Index} / {o.ToString()}");
                            predicted.Add(o.Value);
                        },
                        () =>
                        {
                            if (request.Rename)
                                File.Move(request.Filename, request.Filename.GenerateCopyName(null), true);
                        });

                var result=await reader.Start(cancellationToken);

                await Repository.InsertManyAsync(predicted, true, cancellationToken);

                if (result.Errors.Any())
                    return Result.ValidWithFailure(new LoadPredictedIntoDbResponse(MapToDtoList(predicted)), result.Errors.ToArray());

                return new LoadPredictedIntoDbResponse(MapToDtoList(predicted));
            }
            return Result.Failure<LoadPredictedIntoDbResponse>(new Error($"{request.Filename} n'existe pas"));
        }
        catch (Exception ex)
        {
            return Result.Failure<LoadPredictedIntoDbResponse>(ex);
        }

    }


}
