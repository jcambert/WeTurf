namespace We.Turf.Entities;

#nullable disable
public class StatByDateDto : EntityDto
{
    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public string Pari { get; set; }

    public int Mise { get; set; }
    public double Dividende { get; set; }
}
#nullable restore
