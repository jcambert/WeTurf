using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Notifications;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

public partial class MainLayout : IDisposable
{
    [Inject] private IMediator Mediator { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IMenuStyleProvider MenuStyleManager { get; set; }
    [Inject] private IUiNotificationService UiNotificationService { get; set; }
    [Inject] private IFluidProvider FluidProvider { get; set; }
    private bool IsCollapseShown { get; set; }

    /* protected override void OnInitialized()
     {
     }*/

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        NavigationManager.LocationChanged += OnLocationChanged;
        Style = MenuStyleManager.GetCurrent();
        await Result
            .Create(new GetCurrentMainLayoutFluidCommand())
            .Bind(c => Mediator.Send(c))
            .Match(
                r =>
                {
                    Fluidable = r.Fluidable;
                    return new NoContentResult();
                },
                r =>
                {
                    UiNotificationService.Error(r.Errors.JoinAsString("\n"));
                    return new BadRequestResult();
                }
            );
    }

    public void Dispose()
    {
        if (NavigationManager is not null)
            NavigationManager.LocationChanged -= OnLocationChanged;
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        IsCollapseShown = false;
        InvokeAsync(StateHasChanged);
    }

    public IFluidable Fluidable { get; protected set; }
    public bool IsFluid => Fluidable.IsFluid;
    public IWeMenuStyle Style { get; set; }

    public async Task ChangeFluid(IFluidable fluidable)
    {
        if (fluidable != Fluidable)
        {
            await Result
                .Create(new ApplyMainLayoutFluidifyCommand(fluidable))
                .Bind(c => Mediator.Send(c))
                .Match(
                    r =>
                    {
                        //MainLayout.ChangeFluid(fluidable);
                        //notify.Success("Now it's " + (value ? "Fluid" : "Not Fluid"));
                        return new NoContentResult();
                    },
                    r =>
                    {
                        UiNotificationService.Error(r.Errors.JoinAsString("\n"));
                        return new BadRequestResult();
                    }
                );
            //Fluidable = fluid;
           // UiNotificationService.Success("Set Main Layout to " + Fluidable.Name);

            //StateHasChanged();
        }
    }
    public async Task ChangeFluid(bool fluid)
        => await ChangeFluid(FluidProvider.GetAll().Where(x => x.IsFluid == fluid).FirstOrDefault() ?? FluidProvider.GetDefault());

    public async Task ToggleFluid()
        => await ChangeFluid(!IsFluid);
    public void ChangeStyle(IWeMenuStyle style)
    {
        if (Style != style)
        {
            Style = style;
            StateHasChanged();
        }
    }
}
