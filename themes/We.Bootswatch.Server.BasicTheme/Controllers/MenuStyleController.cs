using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Server.BasicTheme.Controllers;
using We.Results;
using We.Mediatr;
#if MEDIATOR
using Mediator;
#else
using MediatR;
#endif
namespace We.Bootswatch.Server.BasicTheme;

//[Area("Abp")]
[Produces("application/json")]
[Area("Abp")]
[Route("Abp/Style/[action]")]
[ApiVersion("1.0")]
[ApiController]
[ControllerName("MenuStyle")]
public class MenuStyleController : BaseController
{
    public MenuStyleController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public virtual async Task<IActionResult> Change(string style, string returnUrl = "")
    {
        var res = await Result
            .Create(new SetMenuStyleCommand(style))
            .Bind(cmd => Mediator.Send(cmd).AsTaskWrap())
            .Match(
                r => !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(returnUrl) : Redirect("~/"),
                this.HandleFailure
            );

        return res;
    }
}
