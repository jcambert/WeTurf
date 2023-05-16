using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace We.Turf.Blazor.Bundling;

public class BootswatchSciptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        base.ConfigureBundle(context);
        context.Files.Add("/libs/chart.js/chart.js");
    }
}
