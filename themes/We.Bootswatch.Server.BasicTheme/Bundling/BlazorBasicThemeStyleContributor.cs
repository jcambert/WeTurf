using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace We.Bootswatch.Server.BasicTheme;

public class BlazorBasicThemeStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/We.Bootswatch.Components.Web.BasicTheme/libs/we/css/theme.css");
    }
}
