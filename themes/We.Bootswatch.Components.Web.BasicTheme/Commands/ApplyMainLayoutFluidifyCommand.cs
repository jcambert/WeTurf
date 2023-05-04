using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Mediatr;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface IApplyMainLayoutFluidifyCommand : IQuery<ApplyMainLayoutFluidifyResult>
{
    IFluidable Value { get; init; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IApplyMainLayoutFluidifyCommand), IncludeSelf = true)]
public class ApplyMainLayoutFluidifyCommand : IApplyMainLayoutFluidifyCommand
{
    public ApplyMainLayoutFluidifyCommand(IFluidable isFluid)
    {
        Value = isFluid;
    }

    public IFluidable Value { get; init; }
}

public sealed record ApplyMainLayoutFluidifyResult() : Response;
