namespace We.Turf.Entities;

public class MiseDto : EntityDto
{
    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int Somme { get; set; }
}
