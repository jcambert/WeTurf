using System;
using Volo.Abp.Domain.Entities;
using We.Csv;

namespace We.Turf.Entities;

public class Resultat : Entity<Guid>
{
    //date reunion course pari    numPmu rapport
    [CsvField(0)]
    public DateOnly Date { get; set; }
    [CsvField(1)]
    public int Reunion { get; set; }
    [CsvField(2)]
    public int Course { get; set; }
    [CsvField(3)]
    public string Pari { get; set; }
    [CsvField(4)]
    public int NumeroPmu { get; set; }
    [CsvField(5)]
    public double Rapport { get; set; }
}
