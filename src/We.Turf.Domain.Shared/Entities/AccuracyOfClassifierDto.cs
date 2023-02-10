namespace We.Turf.Entities;

[Serializable]
public class AccuracyOfClassifierDto
{
    public string Classifier { get; init; }
    public int PredictionCount { get; init; }
    public int ResultatCount { get; init; }
    public double Percentage => (1.0*ResultatCount) / PredictionCount;
}
