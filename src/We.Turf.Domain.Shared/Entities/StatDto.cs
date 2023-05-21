namespace We.Turf.Entities;

#nullable disable
public class StatDto : EntityDto
{
    public string Classifier { get; set; }
    public string Pari { get; set; }

    public int Mise { get; set; }
    public double Dividende { get; set; }
}
#nullable restore
