using We.Turf.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<PmuScrapAndPredictTodayWorker>()
        .AddTransient<IExecutor, PythonScriptExecutor>(sp =>
        {
            return new PythonScriptExecutor(sp) { WorkingDirectory = @"D:\projets\pmu_scrapper" };
        
        })
        .AddTransient<IAnaconda,Anaconda>()
        .AddTransient<ICommand, AnacondaActivation>()
        .AddTransient<ICommand, PmuScrapToDay>()
        .AddTransient<ICommand, PmuPredictToDay>()
        .AddTransient<ICommand, PmuScrapAndPredictTodayEnded>()
        ;
    })
    .Build();

await host.RunAsync();
