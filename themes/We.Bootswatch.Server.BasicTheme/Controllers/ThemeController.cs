using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Server.BasicTheme.Controllers;
using We.Results;

namespace We.Bootswatch.Server.BasicTheme;

//[Area("Abp")]
//[Route("Abp/Theme/[action]")]


public class ThemeController : BaseController
{


    public ThemeController(IMediator mediator) : base(mediator)
    { }
     
    [HttpGet]
    public virtual async Task<IActionResult> Change([FromQuery] string theme, [FromQuery] string returnUrl)
    {
        var res = await Result
            .Create(new SetThemeCommand(theme))
            .Bind(cmd => Mediator.Send(cmd))
            .Match(
                 r => !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(returnUrl) : Redirect("~/"),
                this.HandleFailure
                
            );
        
        return res;
    }
}
