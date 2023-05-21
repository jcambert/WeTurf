using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Turf.Blazor.Bundling;

public class StyleContributor : BundleContributor
{
    public override Task ConfigureBundleAsync(BundleConfigurationContext context)
    {
        return base.ConfigureBundleAsync(context);
    }

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        base.ConfigureBundle(context);
        var themeProvider = context.LazyServiceProvider.LazyGetRequiredService<IThemeProvider>();
        var theme = themeProvider.GetCurrent();
        /*var httpContext=context.LazyServiceProvider.LazyGetRequiredService<IHttpContextAccessor>().HttpContext;
        if (!httpContext.Request.Cookies.TryGetValue(BootswatchConsts.ThemeCookie, out var theme))
        {
            theme = BootswatchConsts.DefaultTheme;
        }*/
        if (theme is not null)
            context.Files.ReplaceOne(
                "/libs/bootstrap/css/bootstrap.css",
                $"/libs/bootswatch/{theme.Name}/bootstrap.css"
            );

        // context.Files.Add("/libs/chart.js/chart.css");
    }
}
