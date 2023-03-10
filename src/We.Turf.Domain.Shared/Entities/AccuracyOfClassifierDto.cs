namespace We.Turf.Entities;

[Serializable]
public class AccuracyPerClassifierDto
{
    public string Classifier { get; set; }
    public int PredictionCount { get; set; }
    public int ResultatCount { get; set; }
    public double Percentage { get; set; }
}
