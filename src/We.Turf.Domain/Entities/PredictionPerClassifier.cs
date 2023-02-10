using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class PredictionPerClassifier:IEntity
{
    public string Classifier { get; set; }
    public int Counting { get; set; }

    public object[] GetKeys()
    =>new object[] { Classifier };
}
