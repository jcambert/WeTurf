

using Microsoft.Extensions.Options;

namespace We.Turf.Service;

public class PmuWorker : BackgroundService
{
    private bool _processing = false;
    protected ILogger<PmuWorker> Logger { get; init; }

    private IServiceProvider ServiceProvider { get; init; }
    private IServiceScope Scope { get; set; }
    private IObservable<long> Tick { get; set; }
    public IWinCommand Python { get; }
    private int Interval { get; init; } = 5;

    private IDisposable _innerWorker = null;

    private ScrapOptions _options = null;
    public PmuWorker(IServiceProvider serviceProvider)
    {

        this.ServiceProvider = serviceProvider;
        this.Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<PmuWorker>();
        this.Logger.LogInformation("Worker initializing");



        this.Logger.LogInformation($"Worker Set Tick Interval to {Interval}");
        this.Logger.LogInformation("Worker Initialized");

    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Worker Execution starting");

        await Task.Delay(100);
        IServiceScope Scope = ServiceProvider.CreateScope();
        _options = Scope.ServiceProvider.GetService<IOptions<ScrapOptions>>()?.Value ?? new ScrapOptions();
        var python = Scope.ServiceProvider.GetRequiredService<IWinCommand>();
        python.OnOutput.Subscribe(x => Console.WriteLine(x));
        python.Initialize();

        var mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();
        Tick = Observable.Interval(TimeSpan.FromSeconds(_options.Tick));
        _innerWorker = Tick
            .Where(x => !_processing)
            .Where(x => ItsTime(mediator))
            .TakeWhile(x => !stoppingToken.IsCancellationRequested)
            .Subscribe(async (x) => await DoProcessing(x, mediator, stoppingToken), OnCompleted);

        //Tick.TakeWhile(x => !stoppingToken.IsCancellationRequested).Subscribe(x => { Logger.LogInformation(new string('.', ((int)x))); });
        Logger.LogInformation("Worker started");

        /*
        var scrapper = ServiceProvider.GetRequiredService<IExecutor<IPmuScrapAndPredictToday>>();
        scrapper.OnOutput.Subscribe(x => Logger.LogInformation(x));
        await scrapper.Execute(stoppingToken);

        var resultat = ServiceProvider.GetRequiredService<IExecutor<IPmuResultatYesterday>>();
        resultat.OnOutput.Subscribe(x=> Logger.LogInformation(x));  
        await resultat.Execute(stoppingToken);*/

        while (!stoppingToken.IsCancellationRequested)
        {
            //Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(500, stoppingToken);
        }
    }



    private bool ItsTime(IMediator mediator)
    {
        Logger.LogInformation("It's Time ?");
        if (!_options.Enabled)
            return false;

        var dateNow = DateOnly.FromDateTime(DateTime.Now);
        if (_options.LastDate != null && dateNow <= _options.LastDate)
            return false;

        var lastScrapTask = mediator.Send(new GetLastScrappingDateQuery());
        var lastScrap = lastScrapTask.Result;
        if (dateNow <= lastScrap.LastDate)
            return false;


        var timeNow = TimeOnly.FromDateTime(DateTime.Now);
        if (timeNow > _options.TTime)
            return true;

        return false;
    }

    private async Task DoProcessing(long x, IMediator mediator, CancellationToken stoppingToken)
    {
        _processing = true;
        var res0 = await mediator.Send(new PmuScrapTodayQuery());
        var res1 = await mediator.Send(new PmuPredictTodayQuery());
        var res3 = await mediator.Send(new PmuResultatYesterdayQuery());
        var res4 = await mediator.Send(new PmuScrapAndPredictTodayEndedQuery());
        _processing = false;
    }

    private void OnCompleted(Exception obj)
    {
        Logger.LogInformation("Worker Termination Requested");
    }

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
