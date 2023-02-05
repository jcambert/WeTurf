using System;
using System.Collections.Generic;
using System.Text;
using We.Turf.Localization;
using Volo.Abp.Application.Services;

namespace We.Turf;

/* Inherit your application services from this class.
 */
public abstract class TurfAppService : ApplicationService
{
    protected IMediator Mediator => LazyServiceProvider.LazyGetRequiredService<IMediator>();
    protected TurfAppService()
    {
        LocalizationResource = typeof(TurfResource);
    }
}
