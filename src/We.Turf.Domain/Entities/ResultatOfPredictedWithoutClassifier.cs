using System.Diagnostics;
using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

#nullable disable
[DebuggerDisplay("{Date}-{Reunion}-{Course}-{NumeroPmu}-{Pari}-{Dividende}")]
public class ResultatOfPredictedWithoutClassifier : Entity
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NumeroPmu { get; set; }
    public string? Pari { get; set; }

    public double? Dividende { get; set; }

    public override object[] GetKeys() =>
        new object[] { Date, Reunion, Course, NumeroPmu, Pari ?? string.Empty };
}
#nullable restore
