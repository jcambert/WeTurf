using Volo.Abp.Application.Services;
using We.Turf.Localization;

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
