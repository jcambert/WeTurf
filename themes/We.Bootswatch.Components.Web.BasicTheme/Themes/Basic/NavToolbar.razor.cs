using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Volo.Abp.AspNetCore.Components.Web.Security;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

public partial class NavToolbar : IDisposable
{
    [Inject]
    private IToolbarManager? ToolbarManager { get; set; }

    [Inject]
    protected ApplicationConfigurationChangedService? ApplicationConfigurationChangedService { get; set; }

    private List<RenderFragment> ToolbarItemRenders { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        await GetToolbarItemRendersAsync();
        if (ApplicationConfigurationChangedService is not null)

            ApplicationConfigurationChangedService.Changed += ApplicationConfigurationChanged;
    }

    private async Task GetToolbarItemRendersAsync()
    {
        var t = ToolbarManager?.GetAsync(StandardToolbars.Main);
        if (t is null)
            return;
        Toolbar toolbar = await t;
        ToolbarItemRenders.Clear();

        var sequence = 0;
#pragma warning disable ASP0006
        foreach (var item in toolbar.Items)
        {
            ToolbarItemRenders.Add(
                builder =>
                {
                    builder.OpenComponent(sequence++, item.ComponentType);

                    builder.CloseComponent();
                }
            );
        }
    }
#pragma warning restore ASP0006
    private async void ApplicationConfigurationChanged()
    {
        await GetToolbarItemRendersAsync();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        if (ApplicationConfigurationChangedService is not null)
            ApplicationConfigurationChangedService.Changed -= ApplicationConfigurationChanged;
    }
}
