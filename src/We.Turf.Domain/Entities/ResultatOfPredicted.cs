using System;
using Volo.Abp.Domain.Entities;
using We.Csv;

namespace We.Turf.Entities;

public class ResultatOfPredicted : Entity<Guid>
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

    public Guid? Resultat_Id { get; set; }
    public string? Pari { get; set; }
    public double? Dividende { get; set; }
}
