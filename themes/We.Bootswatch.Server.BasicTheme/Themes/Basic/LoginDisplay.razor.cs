using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Volo.Abp.UI.Navigation;

namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

//namespace Volo.Abp.AspNetCore.Components.Server.BasicTheme.Themes.Basic;
public partial class LoginDisplay : IDisposable
{
    [Inject]
    protected IMenuManager? MenuManager { get; set; }

    protected ApplicationMenu? Menu { get; set; }

    protected override async Task OnInitializedAsync()
    {
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
        Menu = await MenuManager?.GetAsync(StandardMenus.User);
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.

        Navigation.LocationChanged += OnLocationChanged;
    }

    protected virtual void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        Navigation.LocationChanged -= OnLocationChanged;
    }
}
