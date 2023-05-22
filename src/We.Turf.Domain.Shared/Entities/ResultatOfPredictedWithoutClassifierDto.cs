using System.Diagnostics;

namespace We.Turf.Entities;

[DebuggerDisplay("{Date}-{Reunion}-{Course}-{NumeroPmu}-{Pari}-{Dividende}")]
[Serializable]
public class ResultatOfPredictedWithoutClassifierDto : EntityDto
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NumeroPmu { get; set; }
    public string? Pari { get; set; }

    public double? Dividende { get; set; }
}
