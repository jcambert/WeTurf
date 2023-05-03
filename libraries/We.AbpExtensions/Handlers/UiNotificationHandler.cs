#if MEDIATR
using MediatR;
#endif
#if MEDIATOR
using Mediator;
#endif
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions.Queries;
using We.Results;

namespace We.AbpExtensions.Handlers;

public class UiNotificationHandler : AbpHandler.With<UiNotificationQuery>
{
    public UiNotificationHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

#if MEDIATOR
    public override async ValueTask<Unit> Handle(
        UiNotificationQuery request,
        CancellationToken cancellationToken
    )
    {
        var (type, message, title, actions) = request;
        Func<string, string, Action<UiNotificationOptions>, Task> fn = type switch
        {
            UiNotificationMessageType.Warning => UiNotification.Warn,
            UiNotificationMessageType.Error => UiNotification.Error,
            UiNotificationMessageType.Information => UiNotification.Info,
            _ => UiNotification.Success
        };

        await fn(message, title, actions);
        return Unit.Value;
    }
#endif
#if MEDIATR
    public override async Task Handle(
        UiNotificationQuery request,
        CancellationToken cancellationToken
    )
    {
        var (type, message, title, actions) = request;
        Func<string, string, Action<UiNotificationOptions>, Task> fn = type switch
        {
            UiNotificationMessageType.Warning => UiNotification.Warn,
            UiNotificationMessageType.Error => UiNotification.Error,
            UiNotificationMessageType.Information => UiNotification.Info,
            _ => UiNotification.Success
        };

        await fn(message, title, actions);
    }
#endif

}
