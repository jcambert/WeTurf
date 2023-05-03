using System;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetTriggerQuery))]
public class GetTriggerQuery : IGetTriggerQuery
{
    public Guid Id { get; }
}
