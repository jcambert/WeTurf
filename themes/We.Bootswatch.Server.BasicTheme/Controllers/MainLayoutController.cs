#if MEDIATOR
using Mediator;
#else
using MediatR;
#endif
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Mediatr;
using We.Results;

namespace We.Bootswatch.Server.BasicTheme.Controllers;

//[Area("Abp")]
//[ApiController]
[Produces("application/json")]
[Area("Abp")]
[Route("Abp/MainLayout/[action]")]
[ApiVersion("1.0")]
[ApiController]
[ControllerName("MainLayout")]
public class MainLayoutController : BaseController
{
    public MainLayoutController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public virtual async Task<IActionResult> Fluid(
        [FromQuery] string isfluid,
        [FromQuery] string returnUrl = ""
    )
    {
        var res = await Result
            .Create(new SetMainLayoutFluidifyCommand(isfluid))
            .Bind(cmd => Mediator.Send(cmd).AsTaskWrap())
            .Match(
                r => !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(returnUrl) : Redirect("~/"),
                this.HandleFailure
            );

        return res;
    }
}
