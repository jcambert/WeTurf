using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class AccuracyPerClassifier : Entity
{
    public string? Classifier { get; set; }
    public int PredictionCount { get; set; }

    public int ResultatCount { get; set; }

    public double Percentage { get; set; }

    public override object[] GetKeys() => new object[] { Classifier ?? string.Empty };
}
