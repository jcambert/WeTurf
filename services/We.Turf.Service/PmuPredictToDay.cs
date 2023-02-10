namespace We.Turf.Service;

public class PmuPredictToDay : AnacondaExecuteStript, IPmuScrapAndPredictToday
{
    public PmuPredictToDay(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "predicter";
        Path = $@"{ExecutingDriveLetter}projets\pmu_scrapper";
    }

}
