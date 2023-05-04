using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Volo.Abp.UI.Navigation;

namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

public partial class FirstLevelNavMenuItem : IDisposable
{
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Parameter]
    public ApplicationMenuItem? MenuItem { get; set; }

    public bool IsSubMenuOpen { get; set; }

    protected override void OnInitialized()
    {
        if (NavigationManager is not null)
            NavigationManager.LocationChanged += OnLocationChanged;
    }

    private void ToggleSubMenu()
    {
        IsSubMenuOpen = !IsSubMenuOpen;
    }

    public void Dispose()
    {
        if (NavigationManager is not null)
            NavigationManager.LocationChanged -= OnLocationChanged;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        IsSubMenuOpen = false;
        InvokeAsync(StateHasChanged);
    }
}
