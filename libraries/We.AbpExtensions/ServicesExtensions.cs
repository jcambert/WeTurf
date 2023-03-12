using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;

namespace We.AbpExtensions;

public static class ServicesExtensions
{
    public static IUiNotificationService NotificationService(this IAbpLazyServiceProvider provider)
        =>provider.LazyGetRequiredService<IUiNotificationService>();
}