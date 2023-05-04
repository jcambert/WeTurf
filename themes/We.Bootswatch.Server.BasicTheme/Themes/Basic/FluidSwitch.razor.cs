using Microsoft.AspNetCore.Components;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;
using We.Results;
using We.Mediatr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;
#if MEDIATOR
using Mediator;
#endif
#if MEDIATR
using MediatR;
#endif
namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

public partial class FluidSwitch
{
    [Inject]
    IAbpLazyServiceProvider? ServiceProvider { get; set; }

    [Inject]
    IMediator? Mediator { get; set; }

    [Inject]
    IFluidProvider? fluidProvider { get; set; }

    [Inject]
    IUiNotificationService? notify { get; set; }
    public bool IsFluid => MainLayout?.IsFluid ?? true;
    public IFluidable? Fluidable => MainLayout?.Fluidable;

    [Parameter]
    public bool ShowText { get; set; }

    [CascadingParameter]
    public MainLayout? MainLayout { get; set; }

    protected string GetText() => ShowText ? (Fluidable?.Name ?? string.Empty) : string.Empty;

    private async Task OnCheckedChange(bool value)
    {
        var fluidable =
            fluidProvider?.GetAll().Where(x => x.IsFluid == value).FirstOrDefault()
            ?? fluidProvider?.GetDefault();
        if (fluidable is not null && Mediator is not null)
            await Result
                .Create(new ApplyMainLayoutFluidifyCommand(fluidable))
                .Bind(c => Mediator.Send(c).AsTaskWrap())
                .Match(
                    r =>
                    {
                        MainLayout?.ChangeFluid(fluidable);
                        //notify.Success("Now it's " + (value ? "Fluid" : "Not Fluid"));
                        return new NoContentResult();
                    },
                    r =>
                    {
                        notify?.Error(r.Errors.JoinAsString("\n"));
                        return new BadRequestResult();
                    }
                );
    }
}
