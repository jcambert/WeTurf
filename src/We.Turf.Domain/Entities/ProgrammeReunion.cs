using System;
using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class ProgrammeReunion:Entity
{
    
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public string Hippodrome { get; set; }
    public string Discipline { get; set; }
    public int Nombre { get; set; }

    public override object[] GetKeys()
    => new object[] { Date ,Reunion,Discipline};


}
