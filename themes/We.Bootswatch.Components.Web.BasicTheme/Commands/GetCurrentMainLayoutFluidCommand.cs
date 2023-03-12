using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using We.Bootswatch.Components.Web.BasicTheme.Services;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface IGetCurrentMainLayoutFluidCommand:ICommand<GetCurrentMainLayoutFluidResult>
{
}
[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetCurrentMainLayoutFluidCommand), IncludeSelf = true)]
public class GetCurrentMainLayoutFluidCommand : IGetCurrentMainLayoutFluidCommand
{
    public GetCurrentMainLayoutFluidCommand()
    {
    }
}

public sealed record GetCurrentMainLayoutFluidResult(IFluidable Fluidable);
