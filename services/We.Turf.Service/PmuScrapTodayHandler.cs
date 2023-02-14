using Polly;
using System.Diagnostics;

namespace We.Turf.Service;
public class ActivateAnaconda : BasePythonConsolePipeline<PmuScrapTodayQuery, PmuScrapTodayResponse>
{
    public ActivateAnaconda(IServiceProvider services) : base(services)
    {
        Python.OnOutput.Subscribe(x=>Console.WriteLine(x));
    }



    public override ValueTask<PmuScrapTodayResponse> Handle(PmuScrapTodayQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<PmuScrapTodayQuery, PmuScrapTodayResponse> next)
    {
        Logger?.LogTrace("Start Anaconda Activation");
        var anacondaActivator = ServiceProvider.GetRequiredService<IAnacondaActivation>();
        anacondaActivator.Send(Python);
        Logger?.LogTrace("End Anaconda Activation");
        return next(message, cancellationToken);
    }
}
public class PmuScrapToday : BasePythonConsolePipeline<PmuScrapTodayQuery, PmuScrapTodayResponse>
{
    public PmuScrapToday(IServiceProvider services) : base(services)
    {
    }

    public override async ValueTask<PmuScrapTodayResponse> Handle(PmuScrapTodayQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<PmuScrapTodayQuery, PmuScrapTodayResponse> next)
    {
        Logger?.LogTrace("Start Pmu Scrapper Today");
        IsProcessing = true;
        var scrapper = ServiceProvider.GetRequiredService<PmuScrapTodayScript>();
        using (var listener = Python
            .OnOutput
            .Where(x => {
#if DEBUG
                Console.WriteLine(x);
                if (x.Trim().ToLower().StartsWith("bye"))
                    Debugger.Break();
#endif
                return x.Trim().ToLower() == "bye..."; 
            })
            .Subscribe(x =>
            {
                IsProcessing = false;
            }))
        {
            scrapper.Send(Python);

            while (IsProcessing)
            {
                await Task.Delay(500);
            }
        }
        Python.OnCompleted();
        Logger?.LogTrace("End Pmu Scrapper Today");
        return await next(message, cancellationToken);
    }
}

public class PmuScrapTodayHandler : BaseRequestHandler<PmuScrapTodayQuery, PmuScrapTodayResponse>
{
    public PmuScrapTodayHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override ValueTask<PmuScrapTodayResponse> Handle(PmuScrapTodayQuery request, CancellationToken cancellationToken)
    {

        return ValueTask.FromResult(new PmuScrapTodayResponse());
    }
}

public class PmuPredictToday : BasePythonConsolePipeline<PmuPredictTodayQuery, PmuPredictTodayResponse>
{
    public PmuPredictToday(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async ValueTask<PmuPredictTodayResponse> Handle(PmuPredictTodayQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<PmuPredictTodayQuery, PmuPredictTodayResponse> next)
    {
        await Task.Delay(100);
        Logger?.LogTrace("Start Pmu Predicter Today");
        var predicter = ServiceProvider.GetRequiredService<PmuPredictTodayScript>();
        predicter.Send(Python);
        Logger?.LogTrace("End Pmu Predicter Today");
        return new PmuPredictTodayResponse();

    }
}

public class PmuPredictTodayHandler : BaseRequestHandler<PmuPredictTodayQuery, PmuPredictTodayResponse>
{
    public PmuPredictTodayHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {

    }

    public override ValueTask<PmuPredictTodayResponse> Handle(PmuPredictTodayQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new PmuPredictTodayResponse());
    }
}