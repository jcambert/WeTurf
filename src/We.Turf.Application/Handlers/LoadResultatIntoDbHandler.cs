using Microsoft.Extensions.Logging;
using System.IO;
using We.Csv;
using We.Turf.Entities;
using System.Reactive.Linq;
using We.Utilities;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class LoadResultatIntoDbHandler : AbpHandler.With<LoadResultatIntoDbQuery, LoadResultatIntoDbResponse>
{
    IRepository<Resultat, Guid> _repository => GetRequiredService<IRepository<Resultat, Guid>>();
    public LoadResultatIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result< LoadResultatIntoDbResponse>> Handle(LoadResultatIntoDbQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(request.Filename))
            {
                var query0 = await _repository.GetQueryableAsync();
                var query1 = query0.Select(x => new { x.Date, x.Reunion, x.Course }).Distinct();
                var existings = await AsyncExecuter.ToListAsync(query1, cancellationToken);

                var reader = new Reader<Resultat>($"{request.Filename}", true, ';');
                List<Resultat> resultats = new List<Resultat>();
                reader
                    .OnReadLine
                    .Where(x => !existings.Any(y => y.Date == x.Value.Date && y.Reunion == x.Value.Reunion && y.Course == x.Value.Course))
                    .Subscribe(o =>
                        {
                            Logger.LogInformation($"{o.Index} / {o.ToString()}");
                            resultats.Add(o.Value);
                        }, () =>
                        {
                            File.Move(request.Filename, request.Filename.GenerateCopyName(null), true);
                        });

                await reader.Start(cancellationToken);

                await _repository.InsertManyAsync(resultats, true, cancellationToken);

                return new LoadResultatIntoDbResponse(ObjectMapper.Map<List<Resultat>, List<ResultatDto>>(resultats));
            }
            return Result.Failure<LoadResultatIntoDbResponse>(new Error( $"{request.Filename} n'existe pas" ));
        }catch(Exception ex)
        {
            return Result.Failure<LoadResultatIntoDbResponse>(ex);
        }
    }
}
