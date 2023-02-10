namespace We.Turf.Service;

public class PmuResultatYesterday : AnacondaExecuteStript, IPmuResultatYesterday
{
    public PmuResultatYesterday(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "resultat";
        Path = $@"{ExecutingDriveLetter}projets\pmu_scrapper";
    }
}
