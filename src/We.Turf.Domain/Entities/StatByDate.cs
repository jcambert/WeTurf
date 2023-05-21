using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

#nullable disable
public class StatByDate : Entity
{
    private string _pari;
    private double _dividende = 0;

    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public string Pari
    {
        get => _pari ?? "E_NON_ARRIVE";
        set { _pari = value; }
    }
    public int Mise { get; set; }
    public double? Dividende
    {
        get => _dividende;
        set { _dividende = value ?? 0.0; }
    }

    public override object[] GetKeys() => new object[] { Date, Classifier, Pari };
}
#nullable restore
