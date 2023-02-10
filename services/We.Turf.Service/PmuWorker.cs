using System.Reactive.Linq;

namespace We.Turf.Service;

public class PmuWorker : BackgroundService
{
    protected ILogger<PmuWorker> Logger { get; init; }
    
    private IServiceProvider ServiceProvider { get; init; }
    public PmuWorker(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        this.Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<PmuWorker>();
        
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scrapper = ServiceProvider.GetRequiredService<IExecutor<IPmuScrapAndPredictToday>>();
        scrapper.OnOutput.Subscribe(x => Logger.LogInformation(x));
        await scrapper.Execute(stoppingToken);

        var resultat = ServiceProvider.GetRequiredService<IExecutor<IPmuResultatYesterday>>();
        resultat.OnOutput.Subscribe(x=> Logger.LogInformation(x));  
        await resultat.Execute(stoppingToken);
     
        //while (!stoppingToken.IsCancellationRequested)
        //{
        //Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //await Task.Delay(1000, stoppingToken);
        //}
    }
}
