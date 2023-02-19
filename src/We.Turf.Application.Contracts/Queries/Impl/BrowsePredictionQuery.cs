using System;
using System.Text.Json.Serialization;
using We.Turf.Converters;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowsePredictionQuery))]
public class BrowsePredictionQuery : IBrowsePredictionQuery
{
    //[JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Date { get; set; }
}
