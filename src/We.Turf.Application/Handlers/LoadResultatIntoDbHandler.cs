using Microsoft.Extensions.Logging;
using We.Csv;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class LoadResultatIntoDbHandler : BaseHandler<LoadResultatIntoDbQuery, LoadResultatIntoDbResponse>
{
    IRepository<Resultat, Guid> _repository => GetRequiredService<IRepository<Resultat, Guid>>();
    public LoadResultatIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<LoadResultatIntoDbResponse> Handle(LoadResultatIntoDbQuery request, CancellationToken cancellationToken)
    {
        request.Filename = @"E:\projets\pmu_scrapper\output\resultats_trot_attele.csv";
        var reader = new Reader<Resultat>($"{request.Filename}", true, ';');
        List<Resultat> resultats = new List<Resultat>();
        reader.OnReadLine.Subscribe(o =>
        {
            Logger.LogInformation($"{o.Index} / {o.ToString()}");
            resultats.Add(o.Value);
        });

        await reader.Start(cancellationToken);

        await _repository.InsertManyAsync(resultats, true, cancellationToken);

        return new LoadResultatIntoDbResponse(ObjectMapper.Map<List<Resultat>, List<ResultatDto>>(resultats));

    }
}
