namespace We.Turf.Service;

public class PmuScrapAndPredictTodayEndedHandler : BaseRequestHandler<PmuScrapAndPredictTodayEndedQuery, PmuScrapAndPredictTodayEndedResponse>
{
    public PmuScrapAndPredictTodayEndedHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override  ValueTask<PmuScrapAndPredictTodayEndedResponse> Handle(PmuScrapAndPredictTodayEndedQuery request, CancellationToken cancellationToken)
    {
        var drive = ServiceProvider.GetRequiredService<DriveService>();
        var mediator=ServiceProvider.GetRequiredService<IMediator>();
        var res0=mediator.Send(new LoadPredictedIntoDbQuery() { Filename=$"{drive}{ScrapConstants.SCRAPPER_PREDICTED}"}).Result;
        var res1=mediator.Send(new LoadResultatIntoDbQuery() { Filename=$"{drive}{ScrapConstants.SCRAPPER_RESULTAT}" }).Result;
        return ValueTask.FromResult( new PmuScrapAndPredictTodayEndedResponse());
    }
}
