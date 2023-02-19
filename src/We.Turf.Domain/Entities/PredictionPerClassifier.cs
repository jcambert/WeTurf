using System;
using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class PredictionPerClassifier:IEntity
{
    public DateOnly Date { get; set; }
    public string Classifier { get; set; }
    public int Counting { get; set; }

    public object[] GetKeys()
    =>new object[] {Date, Classifier };
}
