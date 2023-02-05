using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace We.Turf.Blazor;

[Dependency(ReplaceServices = true)]
public class TurfBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Turf";
}
