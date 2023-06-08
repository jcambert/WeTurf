using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

public partial class ThemeSwitch
{
    private async Task OnChange(
        IAbpLazyServiceProvider service,
        IMediator mediator,
        IThemeProvider provider,
        ITheme item
    )
    {
        var notif = service.NotificationService();
        var res = await Result
            .Create(new ApplyThemeCommand(item.Name))
            .Bind(cmd => mediator.Send(cmd).AsTaskWrap())
            .Match(
                r =>
                {
                    notif.Success("Theme changed");
                    return new NoContentResult();
                },
                r =>
                {
                    notif.Error(r.Errors.AsString());
                    return new BadRequestResult();
                }
            );
    }
}
