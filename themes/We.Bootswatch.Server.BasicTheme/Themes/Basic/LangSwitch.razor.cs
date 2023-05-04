namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

public partial class LangSwitch
{
    private async Task OnChange(
        IAbpLazyServiceProvider service,
        IMediator mediator,
        ILangProvider provider,
        ILang item
    )
    {
        var notif = service.NotificationService();
        var res = await Result
            .Create(new ApplyLanguageCommand(item.CultureName, item.UiCultureName))
            .Bind(cmd => mediator.Send(cmd).AsTaskWrap())
            .Match(
                r =>
                {
                    notif.Success("Language changed");
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
