using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Volo.Abp.AspNetCore.Components.Web.Security;
using Volo.Abp.UI.Navigation;

namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

public partial class NavMenu : IDisposable
{
    [Inject]
    protected IMenuManager? MenuManager { get; set; }

    [Inject]
    protected ApplicationConfigurationChangedService? ApplicationConfigurationChangedService { get; set; }

    protected ApplicationMenu? Menu { get; set; }

    protected async override Task OnInitializedAsync()
    {
        if (MenuManager is not null)
            Menu = await MenuManager.GetMainMenuAsync();
        if (ApplicationConfigurationChangedService is not null)
            ApplicationConfigurationChangedService.Changed += ApplicationConfigurationChanged;
    }

    private async void ApplicationConfigurationChanged()
    {
        if (MenuManager is not null)
            Menu = await MenuManager.GetMainMenuAsync();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        if (ApplicationConfigurationChangedService is not null)
            ApplicationConfigurationChangedService.Changed -= ApplicationConfigurationChanged;
    }
}
