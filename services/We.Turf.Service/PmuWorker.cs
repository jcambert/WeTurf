

namespace We.Turf.Service;

public class PmuWorker : BackgroundService
{
    protected ILogger<PmuWorker> Logger { get; init; }

    private IServiceProvider ServiceProvider { get; init; }
    private IServiceScope Scope { get; set; }
    private IObservable<long> Tick { get; init; }
    public IWinCommand Python { get; }
    private int Interval { get; init; } = 5;

    private IDisposable _innerWorker = null;
    public PmuWorker(IServiceProvider serviceProvider)
    {

        this.ServiceProvider = serviceProvider;
        this.Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<PmuWorker>();
        this.Logger.LogInformation("Worker initializing");
        Tick = Observable.Interval(TimeSpan.FromSeconds(Interval));


        this.Logger.LogInformation($"Worker Set Tick Interval to {Interval}");
        this.Logger.LogInformation("Worker Initialized");

    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Worker Execution starting");

        await Task.Delay(100);
        IServiceScope Scope = ServiceProvider.CreateScope();
        var python = Scope.ServiceProvider.GetRequiredService<IWinCommand>();
        python.OnOutput.Subscribe(x => Console.WriteLine(x));
        python.Initialize();

        var mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();
        var res0 = await mediator.Send(new PmuScrapTodayQuery());
        var res1 = await mediator.Send(new PmuPredictTodayQuery());
        Logger.LogInformation("Worker started");
        //_innerWorker= Tick.Where(x=>!_processing).TakeWhile(x=>!stoppingToken.IsCancellationRequested).Subscribe(async (x)=>await DoProcessing(x, stoppingToken),OnCompleted);
        //Tick.TakeWhile(x => !stoppingToken.IsCancellationRequested).Subscribe(x => { Logger.LogInformation(new string('.',((int) x))); });

        /*
        var scrapper = ServiceProvider.GetRequiredService<IExecutor<IPmuScrapAndPredictToday>>();
        scrapper.OnOutput.Subscribe(x => Logger.LogInformation(x));
        await scrapper.Execute(stoppingToken);

        var resultat = ServiceProvider.GetRequiredService<IExecutor<IPmuResultatYesterday>>();
        resultat.OnOutput.Subscribe(x=> Logger.LogInformation(x));  
        await resultat.Execute(stoppingToken);*/

        //while (!stoppingToken.IsCancellationRequested)
        //{
        //Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //await Task.Delay(1000, stoppingToken);
        //}
    }

    /*private void OnCompleted(Exception obj)
    {
        Logger.LogInformation("Worker Termination Requested");
    }*/

    /*private async Task DoProcessing(object obj, CancellationToken stoppingToken)
    {
        Logger.LogInformation("Worker start Processing");

        //await Task.Delay(TimeSpan.FromSeconds(Interval * 3),stoppingToken);
        await PmuScrapAndPredictToday(stoppingToken);
        //await PmuScrapResultatYesterday(stoppingToken);

        Logger.LogInformation("Worker end Processing");
    }*/

   /* private async Task PmuScrapAndPredictToday(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Worker Scrap and predict Today");
        var scrapper = ServiceProvider.GetRequiredService<IExecutor<IPmuScrapAndPredictToday>>();
        scrapper.OnOutput.Subscribe(x => Logger.LogInformation(x));
        await scrapper.Execute(cancellationToken);

    }*/
    /*
    private async Task PmuScrapResultatYesterday(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Worker Scrap Resultat of Yesterday");
        var resultat = ServiceProvider.GetRequiredService<IExecutor<IPmuResultatYesterday>>();
        resultat.OnOutput.Subscribe(x => Logger.LogInformation(x));
        await resultat.Execute(cancellationToken);
    }*/
    public override void Dispose()
    {
        Logger.LogInformation("Disposing");
        base.Dispose();
        _innerWorker?.Dispose();
        Scope?.Dispose();
        Logger.LogInformation("Disposed");
    }
}
