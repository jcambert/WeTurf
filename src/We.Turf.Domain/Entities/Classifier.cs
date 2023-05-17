using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class Classifier : Entity
{
    public string Name { get; set; } = string.Empty;

    public override object[] GetKeys() => new object[] { Name };
}
