using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;

namespace We.AbpExtensions;

public static class ServicesExtensions
{
    public static IUiNotificationService NotificationService(
        this IAbpLazyServiceProvider provider
    ) => provider.LazyGetRequiredService<IUiNotificationService>();

    public static IAsyncQueryableExecuter AsyncExecuter(this IAbpLazyServiceProvider provider) =>
        provider.LazyGetRequiredService<IAsyncQueryableExecuter>();

    public static IObjectMapper ObjectMapper(
        this IAbpLazyServiceProvider provider,
        Type ObjectMapperContext = null
    ) =>
        provider.LazyGetService<IObjectMapper>(
            p =>
                ObjectMapperContext == null
                    ? p.GetRequiredService<IObjectMapper>()
                    : (IObjectMapper)p.GetRequiredService(
                          typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)
                      )
        );

    public static IMediator Mediator(this IAbpLazyServiceProvider provider) =>
        provider.LazyGetRequiredService<IMediator>();

    public static ICachedServiceProvider Cache(this IAbpLazyServiceProvider provider) =>
        provider.GetRequiredService<ICachedServiceProvider>();

    public static ILoggerFactory LoggerFactory(this IAbpLazyServiceProvider provider) =>
        provider.LazyGetRequiredService<ILoggerFactory>();

    public static ILogger Logger<T>(this IAbpLazyServiceProvider provider) =>
        provider.LazyGetService<ILogger>(
            p => provider.LoggerFactory()?.CreateLogger(typeof(T).FullName) ?? NullLogger.Instance
        );
}
