using System;
using We.Turf.Entities;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IUpdateTriggerQuery))]
public class UpdateTriggerQuery : IUpdateTriggerQuery
{
    public Guid Id { get; set; }
    public TimeOnly Start { get; set; }
}
