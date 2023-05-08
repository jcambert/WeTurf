using We.Turf.Entities;

namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IUpdateParameterQuery))]
public class UpdateParameterQuery : IUpdateParameterQuery
{
    public ParameterDto Parameter { get; set; }
}
