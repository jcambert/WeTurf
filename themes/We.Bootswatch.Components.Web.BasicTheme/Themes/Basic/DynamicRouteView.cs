using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Reflection;

namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class DynamicLayoutAttribute : Attribute
{
    public Type? LayoutType { get; set; }
}

public class DynamicRouteView : RouteView
{
    protected override void Render(RenderTreeBuilder builder)
    {
        var pageLayoutType =
            RouteData.PageType.GetCustomAttribute<LayoutAttribute>()?.LayoutType ?? DefaultLayout;
        var dynAttr = RouteData.PageType.GetCustomAttribute<DynamicLayoutAttribute>();

        base.Render(builder);
    }
}
