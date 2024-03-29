namespace We.Turf.Entities;

[Serializable]
public class AccuracyPerClassifierDto : EntityDto
{
    public string Classifier { get; set; } = string.Empty;
    public int PredictionCount { get; set; }
    public int ResultatCount { get; set; }
    public double Percentage { get; set; }
}
