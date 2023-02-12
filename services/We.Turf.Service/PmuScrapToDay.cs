namespace We.Turf.Service;

public class PmuScrapTodayScript : AnacondaExecuteStript, IPmuScrapAndPredictToday
{
    public PmuScrapTodayScript(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "__init__";
        Path = $@"{ExecutingDriveLetter}projets\pmu_scrapper";
    }

}
