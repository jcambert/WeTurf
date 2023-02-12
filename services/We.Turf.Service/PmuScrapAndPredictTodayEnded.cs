namespace We.Turf.Service;

public class PmuScrapAndPredictTodayEnded : BaseCommand, IPmuScrapAndPredictToday
{
    public PmuScrapAndPredictTodayEnded(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override void Send(TextWriter writer)
    {
        Logger.LogInformation("Ended action");
    }
}
