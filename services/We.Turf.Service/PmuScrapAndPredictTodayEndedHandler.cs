namespace We.Turf.Service;

public class PmuScrapAndPredictTodayEndedHandler : BaseRequestHandler<PmuScrapAndPredictTodayEndedQuery, PmuScrapAndPredictTodayEndedResponse>
{
    public PmuScrapAndPredictTodayEndedHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async ValueTask<PmuScrapAndPredictTodayEndedResponse> Handle(PmuScrapAndPredictTodayEndedQuery request, CancellationToken cancellationToken)
    {
        var drive = ServiceProvider.GetRequiredService<DriveService>();
        var mediator=ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new LoadPredictedIntoDbQuery() { Filename=$"{drive}{ScrapConstants.SCRAPPER_PREDICTED}"});
        await mediator.Send(new LoadResultatIntoDbQuery() { Filename=$"{drive}{ScrapConstants.SCRAPPER_RESULTAT}" });
        return new PmuScrapAndPredictTodayEndedResponse();
    }
}
