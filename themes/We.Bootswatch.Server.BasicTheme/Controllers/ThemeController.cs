using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Server.BasicTheme.Controllers;
using We.Results;

namespace We.Bootswatch.Server.BasicTheme;



[Produces("application/json")]
[Area("Abp")]
[Route("Abp/Theme/[action]")]
[ApiVersion("1.0")]
[ApiController]
[ControllerName("Theme")]
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
