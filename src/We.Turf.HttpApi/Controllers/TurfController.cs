using We.Turf.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace We.Turf.Controllers;

/* Inherit your controllers from this class.
 */
//[ApiController]
public abstract class TurfController : AbpControllerBase
{
    protected TurfController()
    {
        LocalizationResource = typeof(TurfResource);
    }
}
