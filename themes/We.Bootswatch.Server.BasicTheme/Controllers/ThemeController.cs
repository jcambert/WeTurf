using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using We.Bootswatch.Components.Web.BasicTheme;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Result;

namespace We.Bootswatch.Server.BasicTheme;

[Area("Abp")]
[Route("Abp/Theme/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class ThemeController : AbpController
{
    private readonly IMediator _mediator;

    public ThemeController(IMediator mediator)
    {
        this._mediator=mediator;
    }
    [HttpGet("Change")]
    public virtual async Task<IActionResult> Change([FromQuery] string theme,[FromQuery] string returnUrl = "")
    {
        var result=await _mediator.Send(new SetThemeCommand(theme));
        if(result is IFailure)
            return this.BadRequest(result);
        return !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(GetRedirectUrl(returnUrl)) : Redirect("~/");
    }
}
