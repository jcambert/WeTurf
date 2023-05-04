using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Mediatr;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface ISetMainLayoutFluidifyCommand : IQuery<SetMainLayoutFluidifyResult>
{
    string IsFluid { get; }
}

public class SetMainLayoutFluidifyCommand : ISetMainLayoutFluidifyCommand
{
    public SetMainLayoutFluidifyCommand(string isFluid)
    {
        this.IsFluid = isFluid;
    }

    public string IsFluid { get; }
}

public sealed record SetMainLayoutFluidifyResult(IFluidable Fluidable) : Response;
