using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Bootswatch.Server.BasicTheme;

[Area("Abp")]
[Route("Abp/Theme/[action]")]
[DisableAuditing]
[RemoteService(false)]
[ApiExplorerSettings(IgnoreApi = true)]
public class ThemeController : AbpController
{
    private readonly IThemeProvider _themeProvider;

    public ThemeController(IThemeProvider themeProvider)
    {
        this._themeProvider=themeProvider;
    }
    [HttpGet]
    public virtual IActionResult Change(string theme, string returnUrl = "")
    {
        _themeProvider.SetCurrent(theme);
        return !string.IsNullOrWhiteSpace(returnUrl) ? Redirect(GetRedirectUrl(returnUrl)) : Redirect("~/");
    }
}
