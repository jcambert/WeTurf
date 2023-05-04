using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme;
using We.Results;
using We.Results;
using We.AbpExtensions;
using Microsoft.AspNetCore.Mvc;
using We.Mediatr;
#if MEDIATOR
using Mediator;
#endif
#if MEDIATR
using MediatR;
#endif
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
                    notif.Error(r.Errors.JoinAsString("\n"));
                    return new BadRequestResult();
                }
            );
    }
}
