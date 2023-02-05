using We.Turf.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace We.Turf.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TurfController : AbpControllerBase
{
    protected TurfController()
    {
        LocalizationResource = typeof(TurfResource);
    }
}
