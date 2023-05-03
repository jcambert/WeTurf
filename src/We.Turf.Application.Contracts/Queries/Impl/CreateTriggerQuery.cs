using System;
using System.Text.Json.Serialization;
using We.Turf.Entities;
using We.Turf.Converters;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ICreateTriggerQuery))]
public class CreateTriggerQuery : ICreateTriggerQuery
{
    [JsonConverter(typeof(TimeOnlyJsonConverter))]
    public TimeOnly Start { get; set; }
}
