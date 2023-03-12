using We.Bootswatch.Components.Web.BasicTheme.Services;

namespace We.Bootswatch.Components.Web.BasicTheme.Commands;
public interface ISetMainLayoutFluidifyCommand : ICommand<SetMainLayoutFluidifyResult>
{
    string IsFluid { get;  }
}
public class SetMainLayoutFluidifyCommand : ISetMainLayoutFluidifyCommand
{
    public SetMainLayoutFluidifyCommand(string isFluid)
    {
        this.IsFluid = isFluid;
    }
    public string IsFluid{ get;  }
}
public sealed record SetMainLayoutFluidifyResult(IFluidable Fluidable);