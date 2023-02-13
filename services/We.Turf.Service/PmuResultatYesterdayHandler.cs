namespace We.Turf.Service;

public class PmuResultatYesterday : BasePipeline<PmuResultatYesterdayQuery, PmuResultatYesterdayResponse>
{
    public PmuResultatYesterday(IServiceProvider services) : base(services)
    {
    }

    public override ValueTask<PmuResultatYesterdayResponse> Handle(PmuResultatYesterdayQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<PmuResultatYesterdayQuery, PmuResultatYesterdayResponse> next)
    {
        Logger?.LogTrace("Start Pmu Resultat of yesterday");
        var scrapper=ServiceProvider.GetRequiredService<PmuResultatYesterdayScript>();
        scrapper.Send(Python);
        Logger?.LogTrace("End Pmu Resultat of yesterday");
        return next(message, cancellationToken);
    }
}

public class PmuResultatYesterdayHandler : BaseRequestHandler<PmuResultatYesterdayQuery, PmuResultatYesterdayResponse>
{
    public PmuResultatYesterdayHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override ValueTask<PmuResultatYesterdayResponse> Handle(PmuResultatYesterdayQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(new PmuResultatYesterdayResponse());
    }
}
