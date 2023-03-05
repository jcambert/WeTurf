using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using We.Bootswatch.Components.Web.BasicTheme;
using We.Bootswatch.Server.BasicTheme.Services;

namespace We.Bootswatch.Server.BasicTheme;

[Area("Abp")]
[Route("Abp/Style/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class MenuStyleController : AbpController
{
    private readonly IMenuStyleProvider _styleProvider;

    public MenuStyleController(IMenuStyleProvider styleProvider)
    {
        this._styleProvider = styleProvider;
    }
    [HttpGet]
    public virtual IActionResult Change(string style, string returnUrl = "")
    {
        
        _styleProvider.SetCurrent(style);
        return !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(GetRedirectUrl(returnUrl)) : Redirect("~/");
    }
}
