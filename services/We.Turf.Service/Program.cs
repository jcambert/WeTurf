using System.Reflection;
using We.Turf.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<PmuWorker>()
        .AddTransient<DriveService>()
        .AddTransient<IExecutor<IPmuScrapAndPredictToday>, PythonScriptExecutor<IPmuScrapAndPredictToday>>(sp =>
        {
            var drive = sp.GetRequiredService<DriveService>();
            //var location=Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //var drive = new DriveInfo(location);

            return new PythonScriptExecutor<IPmuScrapAndPredictToday>(sp) { WorkingDirectory = $@"{drive}projets\pmu_scrapper" };

        })
        .AddTransient<IExecutor<IPmuResultatYesterday>, PythonScriptExecutor<IPmuResultatYesterday>>(sp =>
        {
            var drive = sp.GetRequiredService<DriveService>();
            return new PythonScriptExecutor<IPmuResultatYesterday>(sp) { WorkingDirectory = $@"{drive}projets\pmu_scrapper" };
        })
        .AddTransient<IAnaconda, Anaconda>()
        .AddTransient<IAnacondaActivation, AnacondaActivation>()
        .AddTransient<IPmuScrapAndPredictToday, PmuScrapToDay>()
        .AddTransient<IPmuScrapAndPredictToday, PmuPredictToDay>()
        .AddTransient<IPmuScrapAndPredictToday, PmuScrapAndPredictTodayEnded>()
        .AddTransient<IPmuResultatYesterday, PmuResultatYesterday>()
        ;
    })
    .Build();

await host.RunAsync();
