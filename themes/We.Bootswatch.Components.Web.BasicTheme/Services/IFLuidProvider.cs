using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace We.Bootswatch.Components.Web.BasicTheme.Services;
public interface IFluidable : INameable
{
    bool IsFluid { get; }
}
public abstract class Fluidable : IFluidable
{
    internal static readonly List<IFluidable> Fluids = new List<IFluidable>();
    internal static readonly IFluidable is_fluid = new Is_Fluid();
    internal static readonly IFluidable not_fluid = new Not_Fluid();
    protected Fluidable(string name, bool value)
    {
        (Name, IsFluid) = (name, value);
        Fluids.Add(this);
    }

    public bool IsFluid { get; }
    public string Name { get; }

    #region internal classes
    private sealed class Is_Fluid : Fluidable
    {
        public Is_Fluid() : base("Fluid", true)
        {

        }
    }
    private sealed class Not_Fluid : Fluidable
    {
        public Not_Fluid() : base("Not Fluid", false)
        {

        }
    }
    #endregion
}
public interface IFluidProvider : ISelectorProvider<IFluidable>
{
}

public class FluidProvider : SelectorProvider<IFluidable>, IFluidProvider
{
    public FluidProvider(IAbpLazyServiceProvider serviceProvider, IHttpContextAccessor context, NavigationManager navigationManager) : base(serviceProvider, context, navigationManager)
    {
    }

    protected override IFluidable Default => Values.FirstOrDefault(t => t.Name == Options.Value.IsFluid) ?? Fluidable.is_fluid;
    protected override List<IFluidable> Values  => Fluidable.Fluids;
    protected override string CookieName => BootswatchConsts.FluidCookie;
}