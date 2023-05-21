using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace We.Turf.Blazor.Bundling;

public class SciptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        base.ConfigureBundle(context);
        context.Files.Add("/libs/chart.js/chart.js");
        var v = context.FileProvider.GetFileInfo("/libs");

        context.Files.Add("/_content/We.Blazor/libs/scroll/wescroll.js");
    }
}
