using System;
using Volo.Abp.Domain.Entities;

namespace We.Turf.Entities;

public class ScrapTrigger : Entity<Guid>
{
    public TimeOnly Start { get; set; }
}
