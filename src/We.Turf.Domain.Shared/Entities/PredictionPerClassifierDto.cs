namespace We.Turf.Entities;

[Serializable]
public class PredictionPerClassifierDto : EntityDto
{
    public string? Classifier { get; set; }
    public int Counting { get; set; }
    public DateOnly Date { get; set; }
}
