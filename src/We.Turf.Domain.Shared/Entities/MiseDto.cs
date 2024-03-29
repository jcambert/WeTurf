using System.Diagnostics;

namespace We.Turf.Entities;

[DebuggerDisplay("{Classifier}-{Reunion}-{Course}-{Somme}")]
public class MiseDto : EntityDto
{
    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int Somme { get; set; }
}
