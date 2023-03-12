using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Server.BasicTheme.Controllers;

[Area("Abp")]
[Route("Abp/MainLayout/[action]")]
public class MainLayoutController : BaseController
{
    public MainLayoutController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public virtual async Task<IActionResult> Fluid([FromQuery] string isfluid,[FromQuery] string returnUrl = "")
    {
        var res = await Result
            .Create(new SetMainLayoutFluidifyCommand(isfluid))
            .Bind(cmd => Mediator.Send(cmd))
            .Match(
                 r => !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(returnUrl) : Redirect("~/"),
                this.HandleFailure

            );

        return res;


    }
}
