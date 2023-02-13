using System;
using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class LastScrapped : IEntity
{
    public DateTime LastDate { get; set; }

    public object[] GetKeys()
    => new object[] { LastDate };
}
