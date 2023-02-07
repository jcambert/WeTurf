using Microsoft.Extensions.Logging;
using We.Csv;
using We.Turf.Entities;

namespace We.Turf.Handlers;



public class LoadPredictedIntoDbHandler : BaseHandler<LoadPredictedIntoDbQuery, LoadPredictedIntoDbResponse>
{
    IRepository<Predicted,Guid> _repository=>GetRequiredService<IRepository<Predicted, Guid>>();

    public LoadPredictedIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<LoadPredictedIntoDbResponse> Handle(LoadPredictedIntoDbQuery request, CancellationToken cancellationToken)
    {
        request.Filename= @"E:\projets\pmu_scrapper\output\predicted.csv";
        var reader = new Reader<Predicted>($"{request.Filename}", true, ';');
        List<Predicted> predicted = new List<Predicted>();
        reader.OnReadLine.Subscribe(o =>
        {
            Logger.LogInformation($"{o.Index} / {o.ToString()}");
            predicted.Add(o.Value);
        });

        await reader.Start(cancellationToken);

        await _repository.InsertManyAsync(predicted,true,cancellationToken);

        return new LoadPredictedIntoDbResponse(ObjectMapper.Map<List<Predicted>, List<PredictedDto>>(predicted));
    }

   
}
