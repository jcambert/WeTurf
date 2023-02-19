using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class AccuracyPerClassifier:IEntity
{
    
    public string Classifier { get; set; }
    public int PredictionCount { get; set; }

    public int ResultatCount { get; set; }

    public double Percentage{ get; set; }

    public object[] GetKeys()
    => new object[] {  Classifier };
}
