using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace We.Turf.Blazor.Bundling;

public class BootswatchStyleContributor : BundleContributor
{

    public override Task ConfigureBundleAsync(BundleConfigurationContext context)
    {
        return base.ConfigureBundleAsync(context);
    }
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        base.ConfigureBundle(context);
        var httpContext=context.LazyServiceProvider.LazyGetRequiredService<IHttpContextAccessor>().HttpContext;
        if (!httpContext.Request.Cookies.TryGetValue(BootswatchConsts.ThemeCookie, out var theme))
        {
            theme = BootswatchConsts.DefaultTheme;
        }

        context.Files.ReplaceOne(
            "/libs/bootstrap/css/bootstrap.css",
            $"/libs/bootswatch/{theme}/bootstrap.css"
        );
    }
}
