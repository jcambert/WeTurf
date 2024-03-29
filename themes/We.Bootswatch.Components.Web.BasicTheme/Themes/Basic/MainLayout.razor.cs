using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Volo.Abp.AspNetCore.Components.Notifications;
using We.AbpExtensions.Queries;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Results;
using We.Mediatr;
#if MEDIATR
using MediatR;
#endif
#if MEDIATOR
using Mediator;
#endif
namespace We.Bootswatch.Components.Web.BasicTheme.Themes.Basic;

#pragma warning disable CS8618
public partial class MainLayout : IDisposable
{
    [Inject]
    private IMediator? Mediator { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private IMenuStyleProvider? MenuStyleManager { get; set; }

    [Inject]
    private IUiNotificationService? UiNotificationService { get; set; }

    [Inject]
    private IFluidProvider? FluidProvider { get; set; }
    private bool IsCollapseShown { get; set; }

    /* protected override void OnInitialized()
     {
     }*/
    string InitializationError = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (NavigationManager is not null)
            NavigationManager.LocationChanged += OnLocationChanged;
        if (MenuStyleManager is not null)
            Style = MenuStyleManager.GetCurrent();
        if (Mediator is not null)
        {
            var result = await Result
                .Create(new GetCurrentMainLayoutFluidCommand())
                .Bind(c => Mediator.Send(c).AsTaskWrap())
                .Match(
                    r =>
                    {
                        Fluidable = r.Fluidable;
                        return r.AsActionResult();
                    },
                    (Result r) =>
                    {
                        InitializationError = r.Errors.AsString();
                        return r.AsActionResult();
                    }
                );
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && Mediator is not null)
        {
            await Task.Delay(200);

            await Mediator.Send(UiNotificationQuery.Info("Page Loaded", "Message"));

            if (!string.IsNullOrEmpty(InitializationError))
                await Mediator.Send(UiNotificationQuery.Error(InitializationError, "Error"));
        }
    }

    public void Dispose()
    {
        if (NavigationManager is not null)
            NavigationManager.LocationChanged -= OnLocationChanged;
        GC.SuppressFinalize(this);
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        IsCollapseShown = false;
        InvokeAsync(StateHasChanged);
    }

    public IFluidable? Fluidable { get; protected set; }
    public bool IsFluid => Fluidable?.IsFluid ?? false;
    public IWeMenuStyle? Style { get; private set; }

    public async Task ChangeFluid(IFluidable? fluidable)
    {
        if (fluidable is not null && fluidable != Fluidable && Mediator is not null)
        {
            await Result
                .Create(new ApplyMainLayoutFluidifyCommand(fluidable))
                .Bind(c => Mediator.Send(c).AsTaskWrap())
                .MatchAsync(
                    r => r.AsActionResultAsync(),
                    async r =>
                    {
                        await Mediator.Send(UiNotificationQuery.Error(r.Errors.AsString()));
                        return r.AsActionResult();
                    }
                );
        }
    }

    public async Task ChangeFluid(bool fluid) =>
        await ChangeFluid(
            FluidProvider?.GetAll().Where(x => x.IsFluid == fluid).FirstOrDefault()
                ?? FluidProvider?.GetDefault()
        );

    public async Task ToggleFluid() => await ChangeFluid(!IsFluid);

    public void ChangeStyle(IWeMenuStyle style)
    {
        if (Style != style)
        {
            Style = style;
            StateHasChanged();
        }
    }
}
#pragma warning restore CS8618
