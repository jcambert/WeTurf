namespace We.Turf.Entities;

public class DividendeDto : EntityDto
{
    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public double Somme { get; set; }
}
