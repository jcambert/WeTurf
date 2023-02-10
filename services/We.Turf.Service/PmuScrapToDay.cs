namespace We.Turf.Service;

public class PmuScrapToDay : AnacondaExecuteStript, IPmuScrapAndPredictToday
{
    public PmuScrapToDay(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "__init__";
        Path = $@"{ExecutingDriveLetter}projets\pmu_scrapper";
    }

}
