namespace We.Turf.Entities;

[Serializable]
public class PredictionByDateDto : EntityDto
{
    public DateOnly Date { get; set; }
    public int Reunion { get; set; }
    public int Course { get; set; }
    public int NumeroPmu { get; set; }
}
