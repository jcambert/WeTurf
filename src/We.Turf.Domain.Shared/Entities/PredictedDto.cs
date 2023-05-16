namespace We.Turf.Entities;

[Serializable]
public class PredictedDto : EntityDto<Guid>
{
    public string? Classifier { get; set; }
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NumeroPmu { get; set; }
    public string? Nom { get; set; }
    public double Rapport { get; set; }
    public string? Specialite { get; set; }
    public string? Hippodrome { get; set; }
    public int Place { get; set; }
    public double DividenceGagnant { get; set; }
    public double DividencePlace { get; set; }
}

[Serializable]
public class PredictedOnlyDto : EntityDto<Guid>
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NumeroPmu { get; set; }

    public string Hash => $"{Reunion}-{Course}-{NumeroPmu}";
}
