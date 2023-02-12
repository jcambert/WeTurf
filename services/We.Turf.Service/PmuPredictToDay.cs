namespace We.Turf.Service;

public class PmuPredictTodayScript : AnacondaExecuteStript, IPmuScrapAndPredictToday
{
    public PmuPredictTodayScript(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "predicter";
        Path = $@"{ExecutingDriveLetter}projets\pmu_scrapper";
    }

}
