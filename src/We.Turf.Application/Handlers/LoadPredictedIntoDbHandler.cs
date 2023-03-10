using Microsoft.Extensions.Logging;
using System.IO;
using System.Reactive.Linq;
using We.Csv;
using We.Turf.Entities;
using We.Utilities;

namespace We.Turf.Handlers;



public class LoadPredictedIntoDbHandler : BaseHandler<LoadPredictedIntoDbQuery, LoadPredictedIntoDbResponse>
{
    IRepository<Predicted, Guid> _repository => GetRequiredService<IRepository<Predicted, Guid>>();

    public LoadPredictedIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<LoadPredictedIntoDbResponse> Handle(LoadPredictedIntoDbQuery request, CancellationToken cancellationToken)
    {
        if (File.Exists(request.Filename))
        {

            var query0 = await _repository.GetQueryableAsync();
            var query1 = query0.Select(x => new { x.Date, x.Reunion, x.Course }).Distinct();
            var existings = await AsyncExecuter.ToListAsync(query1, cancellationToken);

            var reader = new Reader<Predicted>($"{request.Filename}", true, ';');
            List<Predicted> predicted = new List<Predicted>();
            reader
                .OnReadLine
                .Where(x=>!existings.Any(y => y.Date == x.Value.Date && y.Reunion == x.Value.Reunion && y.Course == x.Value.Course))
                .Subscribe(o =>
                    {
                        Logger.LogInformation($"{o.Index} / {o.ToString()}");
                        predicted.Add(o.Value);
                    },
                    () =>
                    {
                        File.Move(request.Filename, request.Filename.GenerateCopyName(null), true);
                    });

            await reader.Start(cancellationToken);

            await _repository.InsertManyAsync(predicted, true, cancellationToken);

            return new LoadPredictedIntoDbResponse(ObjectMapper.Map<List<Predicted>, List<PredictedDto>>(predicted));
        }
        return new LoadPredictedIntoDbResponse(null) { ErrorMessage = $"{request.Filename} n'existe pas" };

    }


}
