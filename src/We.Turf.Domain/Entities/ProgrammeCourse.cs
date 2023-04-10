using System;
using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class ProgrammeCourse:Entity<Guid>
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public string Discipline { get; set; }
    public string Libelle { get; set; }
    public int Distance { get; set; }

    public string DistanceUnite { get; set; }
    public int NombrePartants { get; set; }
    public string OrdreArrivee { get; set; }
    public string  Hippodrome{ get; set; }
    public override object[] GetKeys()
     => new object[] {Id };
}
