using Microsoft.Extensions.Logging;
using System.IO;
using We.AbpExtensions;
using We.Results;
using We.Utilities;

namespace We.Turf.Handlers;

public class LoadOutputFolderIntoDbHandler : AbpHandler.With<LoadOutputFolderIntoDbQuery, LoadOutputFolderIntoDbResponse>
{
    private readonly List<Error> _errors = new ();
    public LoadOutputFolderIntoDbHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    private void HandleResultat<T>(Result<T> result)
    {
        if (!result)
        {
            _errors.AddRange(result.Errors);
        }
    }
#if MEDIATOR
    public override async ValueTask<Result<LoadOutputFolderIntoDbResponse>> Handle(LoadOutputFolderIntoDbQuery request, CancellationToken cancellationToken)
#else
    public override async Task<Result<LoadOutputFolderIntoDbResponse>> Handle(LoadOutputFolderIntoDbQuery request, CancellationToken cancellationToken)
#endif
    {
        LogTrace($"Start Handling {nameof(LoadOutputFolderIntoDbHandler)}");
        try
        {
            if (Directory.Exists(request.Folder))
            {
                var files = Directory.EnumerateFiles(request.Folder, "*.csv");
                foreach (var file in files)
                {
                    Filename filename = file;
                    if (filename == null)
                    {

                        LogError($"{file} cannot be implicited  convert to Filename Object");
                    }
                    else
                    {
                        LogTrace($"Try Load {file}");
                        if (filename.Name.StartsWith("resultats"))
                        {
                            var q = new LoadResultatIntoDbQuery() { Filename = file,Rename=false };
                            var res = await Mediator.Send(q, cancellationToken);
                            HandleResultat(res);
                            LogTrace($" {file} Loaded");
                        }
                        if (filename.Name.StartsWith("courses"))
                        {
                            var q = new LoadCourseIntoDbQuery() { Filename = file,Rename=false };
                            var res = await Mediator.Send(q, cancellationToken);
                            HandleResultat(res);
                            LogTrace($" {file} Loaded");
                        }
                        if (filename.Name.StartsWith("predicted"))
                        {
                            var q = new LoadPredictedIntoDbQuery() { Filename = file,Rename= false };
                            var res = await Mediator.Send(q, cancellationToken);
                            HandleResultat(res);
                            LogTrace($" {file} Loaded");
                        }
                    }
                }
                if (_errors.Any())
                {
                    LogTrace($"Loaded succeded with some errors");
                    return Result.ValidWithFailure(new LoadOutputFolderIntoDbResponse(), _errors.ToArray());

                }

                LogTrace($"Loaded succeded ");
                return new LoadOutputFolderIntoDbResponse();
            }
            LogWarning($"{request.Folder} n'existe pas");
            return Result.Failure<LoadOutputFolderIntoDbResponse>(new Error($"{request.Folder} n'existe pas"));
        }
        catch (Exception ex)
        {
            LogError(ex.Message);
            return Result.Failure<LoadOutputFolderIntoDbResponse>(ex);
        }

    }
}
