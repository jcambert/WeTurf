using Microsoft.Extensions.Logging;
using System.Reactive.Linq;
using We.Csv;
using We.Utilities;

namespace We.Turf.Handlers;

public class LoadPredictedIntoDbHandler
    : AbpHandler.With<
          LoadPredictedIntoDbQuery,
          LoadPredictedIntoDbResponse,
          Predicted,
          PredictedDto,
          Guid
      >,
      IDisposable
{
    private IDisposable? readerDisposable;
    private bool disposedValue;

    ICsvReader<Predicted> _reader { get; init; }

    public LoadPredictedIntoDbHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        _reader = serviceProvider.GetRequiredService<ICsvReader<Predicted>>();
    }

    protected override async Task<Result<LoadPredictedIntoDbResponse>> InternalHandle(
        LoadPredictedIntoDbQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (File.Exists(request.Filename))
            {
                var query0 = await Repository.GetQueryableAsync();
                var query1 = query0.Select(x => new { x.Date, x.Reunion, x.Course }).Distinct();
                var existings = await AsyncExecuter.ToListAsync(query1, cancellationToken);
                _reader.Filename = request.Filename;
                _reader.HasHeader = request.HasHeader;
                _reader.Separator = request.Separator;
                //var reader = new Reader<Predicted>($"{request.Filename}", true, ';');
                List<Predicted> predicted = new();
                readerDisposable = _reader.OnReadLine
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
                            predicted.Add(o.Value);
                        } /*,
                        () =>
                        {
                            if (request.Rename)
                                File.Move(
                                    request.Filename,
                                    request.Filename.GenerateCopyName(null),
                                    true
                                );
                        }*/
                    );

                var result = await _reader.Start(cancellationToken);

                await Repository.InsertManyAsync(predicted, true, cancellationToken);

                if (request.Rename)
                    File.Move(request.Filename, request.Filename.GenerateCopyName(null), true);

                if (result.Errors.Any())
                    return Result.ValidWithFailure(
                        new LoadPredictedIntoDbResponse(MapToDtoList(predicted)),
                        result.Errors.ToArray()
                    );

                return new LoadPredictedIntoDbResponse(MapToDtoList(predicted));
            }
            return Result.Failure<LoadPredictedIntoDbResponse>(
                new Error($"{request.Filename} n'existe pas")
            );
        }
        catch (Exception ex)
        {
            return Result.Failure<LoadPredictedIntoDbResponse>(ex);
        }
    }

    #region IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                readerDisposable?.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Ne changez pas ce code. Placez le code de nettoyage dans la mActhode 'Dispose(bool disposing)'
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
