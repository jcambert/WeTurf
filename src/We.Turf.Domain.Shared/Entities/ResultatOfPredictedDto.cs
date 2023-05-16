using System.Diagnostics;

namespace We.Turf.Entities;

[Serializable]
public class ResultatOfPredictedDto : EntityDto<Guid>
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

    public Guid Resultat_Id { get; set; }
    public string? Pari { get; set; }
    public double Dividende { get; set; }
}

[Serializable]
[DebuggerDisplay("{Reunion}-{Course}-{NumeroPmu}-{Dividende}")]
public class ResultatOfPredictedStatisticalDto : EntityDto<Guid>
{
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NumeroPmu { get; set; }
    public string? Nom { get; set; }
    public double Rapport { get; set; }

    public string? Pari { get; set; }
    public double Dividende { get; set; }
}
