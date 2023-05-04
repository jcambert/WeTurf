using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

public partial class MenuStyleSelector
{
    private async Task OnChange(
        IAbpLazyServiceProvider service,
        IMediator mediator,
        IMenuStyleProvider provider,
        IWeMenuStyle item
    )
    {
        var notif = service.NotificationService();
        var res = await Result
            .Create(new ApplyMenuStyleCommand(item.Name))
            .Bind(cmd => mediator.Send(cmd).AsTaskWrap())
            .Match(
                r =>
                {
                    notif.Success("Menu changed");
                    return new NoContentResult();
                },
                r =>
                {
                    notif.Error(r.Errors.JoinAsString("\n"));
                    return new BadRequestResult();
                }
            );
    }
}
