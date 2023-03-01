using System.Collections.Generic;
using System.Threading.Tasks;
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
        var theme = "darkly";

        context.Files.ReplaceOne(
            "/libs/bootstrap/css/bootstrap.css",
            $"/libs/bootswatch/{theme}/bootstrap.css"
        );
    }
}
