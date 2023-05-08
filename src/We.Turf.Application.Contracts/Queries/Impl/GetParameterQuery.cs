namespace We.Turf.Queries;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetParameterQuery))]
public class GetParameterQuery : IGetParameterQuery { }
