using System;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IDeleteTriggerQuery))]
public class DeleteTriggerQuery : IDeleteTriggerQuery
{
    public Guid Id { get; set; }
}
