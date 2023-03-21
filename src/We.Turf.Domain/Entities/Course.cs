using System;
using Volo.Abp.Domain.Entities;
using We.Csv;

namespace We.Turf.Entities;

public class Course : Entity<Guid>
{
    [CsvField(0)]
    public int Reunion { get; set; }
    [CsvField(1)]
    public int Numero { get; set; }
    [CsvField(2)]
    public  string  Libelle { get; set; }
    [CsvField(3)]
    public string LibelleCourt{ get; set; }
    [CsvField(4)]
    public int MontantPrix { get; set; }
    [CsvField(5)]
    public int Distance { get; set; }
    [CsvField(6)]
    public string DistanceUnite { get; set; }
    [CsvField(7)]
    public string Discipline { get; set; }
    [CsvField(8)]
    public string Specialite { get; set; }
    [CsvField(9)]
    public int NombrePartants { get; set; }
    [CsvField(10)]
    public string OrdreArrivee { get; set; }
    [CsvField(11)]
    public string HippoCode { get; set; }
    [CsvField(12)]
    public string HippoCourt { get; set; }
    [CsvField(13)]
    public string HippoLong { get; set; }
    [CsvField(14)]
    public DateOnly Date { get; set; }

}
