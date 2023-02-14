using We.Turf.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((ctx, cfg) =>
    {
        
    })
    .ConfigureServices( (ctx,services) =>
    {
        
        services.AddHttpClient();
        services.AddHttpClient(HttpClientApi.NAME, b => {
            b.BaseAddress = new Uri("https://localhost:44381/");
            
        })
        .AddPolicyHandler(PollyPolicies.RetryPolicy)
        .AddPolicyHandler(PollyPolicies.CircuitBreakerPolicy)
        ;

        services
        .AddHostedService<PmuWorker>()
        .Configure<ScrapOptions>( ctx.Configuration.GetSection(ScrapOptions.SCRAP_OPTIONS))
        
        .AddTransient<DriveService>()
        .AddTransient<IExecutor<IPmuScrapAndPredictToday>, PythonScriptExecutor<IPmuScrapAndPredictToday>>(sp =>
        {
            var drive = sp.GetRequiredService<DriveService>();

            return new PythonScriptExecutor<IPmuScrapAndPredictToday>(sp) { WorkingDirectory = $@"{drive}{ScrapConstants.SCRAPPER_WORKING_DIRECTORY} " };

        })
        .AddTransient<IExecutor<IPmuResultatYesterday>, PythonScriptExecutor<IPmuResultatYesterday>>(sp =>
        {
            var drive = sp.GetRequiredService<DriveService>();
            return new PythonScriptExecutor<IPmuResultatYesterday>(sp) { WorkingDirectory = $@"{drive}{ScrapConstants.SCRAPPER_WORKING_DIRECTORY}" };
        })
        .AddTransient<TestScript>()
        .AddTransient<IAnaconda, Anaconda>()
        .AddTransient<IAnacondaActivation, AnacondaActivation>()
        .AddTransient<IPmuScrapAndPredictToday, PmuScrapTodayScript>()
        .AddTransient<PmuScrapTodayScript>()
        .AddTransient<IPmuScrapAndPredictToday, PmuPredictTodayScript>()
        .AddTransient<PmuPredictTodayScript>()
        .AddTransient<IPmuScrapAndPredictToday, PmuScrapAndPredictTodayEnded>()
        .AddTransient<IPmuResultatYesterday, PmuResultatYesterdayScript>()
        .AddMediator(options =>
        {
            options.Namespace = "We.Turf.Service";
            options.ServiceLifetime = ServiceLifetime.Transient;
        })
        .AddTransient<IPipelineBehavior<PmuScrapTodayQuery, PmuScrapTodayResponse>, ActivateAnaconda>()
        .AddTransient<IPipelineBehavior<PmuScrapTodayQuery, PmuScrapTodayResponse>, PmuScrapToday>()
        .AddTransient<IPipelineBehavior<PmuPredictTodayQuery, PmuPredictTodayResponse>, PmuPredictToday>()
        .AddScoped<IWinCommand, WinCommand>(sp =>
        {
            var drive = sp.GetRequiredService<DriveService>();
            return new WinCommand(sp) { WorkingDirectory = $@"{drive}projets\pmu_scrapper" };
        });
        ;
    })
    .Build();

await host.RunAsync();

