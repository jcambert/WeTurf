using We.Turf.Localization;
using Volo.Abp.AspNetCore.Components;

namespace We.Turf.Blazor;

public abstract class TurfComponentBase : AbpComponentBase
{
    protected TurfComponentBase()
    {
        LocalizationResource = typeof(TurfResource);
    }
}
