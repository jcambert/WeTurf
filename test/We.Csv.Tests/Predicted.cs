using System.Diagnostics;

namespace We.Csv.Tests;

[DebuggerDisplay(
    "{Classifier}-{Date}-{Reunion}-{Course}-{NumeroPmu}-{Nom}-{Rapport}-{Specialite}-{Hippodrome}"
)]
public class Predicted
{
    //;index_classifier;date;reunion;course;numPmu;place;nom;rapport;specialite;hippo_code
    [CsvField(1)]
    public string Classifier { get; set; }

    [CsvField(2)]
    public string Date { get; set; }

    [CsvField(3)]
    public int Reunion { get; set; }

    [CsvField(4)]
    public int Course { get; set; }

    [CsvField(5)]
    public int NumeroPmu { get; set; }

    [CsvField(7)]
    public string Nom { get; set; }

    [CsvField(8)]
    public double Rapport { get; set; }

    [CsvField(9)]
    public string Specialite { get; set; }

    [CsvField(10)]
    public string Hippodrome { get; set; }

    public override string ToString() =>
        $"{Classifier}-{Date}-{Reunion}-{Course}-{NumeroPmu}-{Nom}-{Rapport}-{Specialite}-{Hippodrome}";
}
