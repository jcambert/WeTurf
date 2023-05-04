using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Mediatr;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface IGetCurrentMainLayoutFluidCommand : IQuery<GetCurrentMainLayoutFluidResult> { }

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetCurrentMainLayoutFluidCommand), IncludeSelf = true)]
public class GetCurrentMainLayoutFluidCommand : IGetCurrentMainLayoutFluidCommand
{
    public GetCurrentMainLayoutFluidCommand() { }
}

public sealed record GetCurrentMainLayoutFluidResult(IFluidable Fluidable) : Response;
