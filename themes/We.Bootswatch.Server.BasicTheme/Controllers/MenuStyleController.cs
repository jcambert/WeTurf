using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Server.BasicTheme.Controllers;
using We.Results;

namespace We.Bootswatch.Server.BasicTheme;

//[Area("Abp")]
//[Route("Abp/Style/[action]")]
public class MenuStyleController : BaseController
{
    

    public MenuStyleController(IMediator mediator):base(mediator)
    {
    }
    [HttpGet]
    public virtual async Task<IActionResult> Change(string style, string returnUrl = "")
    {
        var res = await Result
            .Create(new SetMenuStyleCommand(style))
            .Bind(cmd => Mediator.Send(cmd))
            .Match(
                 r => !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(returnUrl) : Redirect("~/"),
                this.HandleFailure

            );
        
        return res;

      
    }
}
