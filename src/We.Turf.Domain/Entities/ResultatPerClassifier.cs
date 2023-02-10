using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class ResultatPerClassifier:IEntity
{
    public string Classifier { get; set; }
    public int Counting { get; set; }

    public object[] GetKeys()
    =>new object[] { Classifier };
}
