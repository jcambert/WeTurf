using Microsoft.Extensions.Logging;
using System.IO;
using We.Csv;
using We.Turf.Entities;
using System.Reactive.Linq;
using We.Utilities;
using We.AbpExtensions;
using We.Results;

namespace We.Turf.Handlers;

public class LoadResultatIntoDbHandler : AbpHandler.With<LoadResultatIntoDbQuery, LoadResultatIntoDbResponse, Resultat, ResultatDto, Guid>
{
    public LoadResultatIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task<Result<LoadResultatIntoDbResponse>> Handle(LoadResultatIntoDbQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(request.Filename))
            {
                var query0 = await Repository.GetQueryableAsync();
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
                            if (request.Rename)
                                File.Move(request.Filename, request.Filename.GenerateCopyName(null), true);
                        });

                var result = await reader.Start(cancellationToken);

                await Repository.InsertManyAsync(resultats, true, cancellationToken);
                if (result.Errors.Any())
                    return Result.ValidWithFailure(new LoadResultatIntoDbResponse(MapToDtoList(resultats)), result.Errors.ToArray());

                return new LoadResultatIntoDbResponse(MapToDtoList(resultats));
            }
            return Result.Failure<LoadResultatIntoDbResponse>(new Error($"{request.Filename} n'existe pas"));
        }
        catch (Exception ex)
        {
            return Result.Failure<LoadResultatIntoDbResponse>(ex);
        }
    }
}
