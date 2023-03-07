using Blazorise;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using We.Bootswatch.Components.Web.BasicTheme;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Server.BasicTheme.Services;
using We.Result;

namespace We.Bootswatch.Server.BasicTheme;

[Area("Abp")]
[Route("Abp/Style/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class MenuStyleController : AbpController
{
    private readonly IMediator _mediator;

    public MenuStyleController(IMediator mediator)
    {
        this._mediator = mediator;
    }
    [HttpGet]
    public virtual async Task<IActionResult> Change(string style, string returnUrl = "")
    {
        var result = await _mediator.Send(new SetMenuStyleCommand(style));
        if (result is IFailure)
            return this.BadRequest(result);
        return !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(GetRedirectUrl(returnUrl)) : Redirect("~/");

      
    }
}
