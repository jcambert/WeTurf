using Dawn;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;
using We.Mediatr;

namespace We.AbpExtensions.Queries;

public enum UiNotificationMessageType
{
    Error,
    Warning,
    Information,
    Success
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IUiNotificationQuery))]
public interface IUiNotificationQuery : IQuery
{
    /// <summary>
    /// Type of Message you want notify
    /// </summary>
    UiNotificationMessageType MessageType { get; }

    /// <summary>
    /// The message to notify
    /// </summary>
    string Message { get; }

    /// <summary>
    /// The title of notification
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Options of the notifier
    /// </summary>
    public Action<UiNotificationOptions> Options { get; }
}

public class UiNotificationQuery : IUiNotificationQuery
{
    public static UiNotificationQuery Error(
        string message,
        string title = null,
        Action<UiNotificationOptions> options = null
    ) => new UiNotificationQuery(UiNotificationMessageType.Error, message, title, options);

    public static UiNotificationQuery Success(
        string message,
        string title = null,
        Action<UiNotificationOptions> options = null
    ) => new UiNotificationQuery(UiNotificationMessageType.Success, message, title, options);

    public static UiNotificationQuery Info(
        string message,
        string title = null,
        Action<UiNotificationOptions> options = null
    ) => new UiNotificationQuery(UiNotificationMessageType.Information, message, title, options);

    public static UiNotificationQuery Warn(
        string message,
        string title = null,
        Action<UiNotificationOptions> options = null
    ) => new UiNotificationQuery(UiNotificationMessageType.Warning, message, title, options);

    public UiNotificationQuery(
        UiNotificationMessageType messageType,
        string message,
        string title = null,
        Action<UiNotificationOptions> options = null
    )
    {
        Guard.Argument(message, nameof(message)).NotNull().NotEmpty();
        MessageType = messageType;
        Message = message;
        Title = title;
        Options = options;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public UiNotificationMessageType MessageType { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Action<UiNotificationOptions> Options { get; }

    public void Deconstruct(
        out UiNotificationMessageType type,
        out string message,
        out string title,
        out Action<UiNotificationOptions> options
    )
    {
        type = MessageType;
        message = Message;
        title = Title;
        options = Options;
    }
}

//public sealed record UiNotificationResponse():Response;
