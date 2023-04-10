using System;
using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class LastScrapped : Entity
{
    public DateTime LastDate { get; set; }

    public override object[] GetKeys()
    => new object[] { LastDate };
}
