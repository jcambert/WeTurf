namespace We.Turf.Service;

public class PmuResultatYesterdayScript : AnacondaExecuteStript, IPmuResultatYesterday
{
    public PmuResultatYesterdayScript(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "resultat";
        Path = $@"{ExecutingDriveLetter}projets\pmu_scrapper";
    }
}
