using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

#nullable disable
public class PredictionByDate : Entity
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NumeroPmu { get; set; }

    public override object[] GetKeys() => new object[] { Date, Reunion, Course, NumeroPmu };
}

#nullable restore
