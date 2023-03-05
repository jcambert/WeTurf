using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;

namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

public partial class MainLayout : IDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IMenuStyleProvider MenuStyleManager { get; set; }
    private bool IsCollapseShown { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
        Style = MenuStyleManager.GetCurrent();
    }

    

    public void Dispose()
    {
        if(NavigationManager is not null)
            NavigationManager.LocationChanged -= OnLocationChanged;
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        IsCollapseShown = false;
        InvokeAsync(StateHasChanged);
    }

    
    public bool IsFluid { get; protected set; } = true;
    public IWeMenuStyle Style { get;  set; }

    public void ChangeFluid(bool isFluid)
    {
        if(isFluid!=IsFluid) {
            IsFluid = isFluid;
            StateHasChanged();
        }
    }

    public void ChangeStyle(IWeMenuStyle style)
    {
        if (Style != style)
        {
            Style = style;
            StateHasChanged();
        }
    }
}
