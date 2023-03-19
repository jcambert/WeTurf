using MediatR;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using We.Mediatr;

namespace We.AbpExtensions;
/*
public interface IQueryHandler<TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery
{

}
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : Response
{

}*/

public interface IBaseHandler
{
     IAbpLazyServiceProvider LazyServiceProvider { get;  }
     IAsyncQueryableExecuter AsyncExecuter { get; }

     Type ObjectMapperContext { get; set; }
     IObjectMapper ObjectMapper { get; }

    IMediator Mediator { get; }

    T GetRequiredService<T>();

     ICachedServiceProvider Cache { get; }

    IUiNotificationService UiNotification { get; }

    #region Logging
    ILoggerFactory LoggerFactory { get; }
    ILogger Logger { get; }

     void LogTrace(string message, params object[] args) ;
     void LogCritical(string message, params object[] args) ;
     void LogDebug(string message, params object[] args) ;
     void LogError(string message, params object[] args) ;
     void LogException(Exception ex, LogLevel? logLevel = null) ;
     void LogInformation(string message, params object[] args) ;
     void LogWarning(string message, params object[] args);
     void LogWithLevel(LogLevel logLevel, string message) ;

    #endregion


}

public static class AbpHandler
{
    public abstract class With<TQuery> : Handler.With<TQuery>, IBaseHandler
        where TQuery : IQuery
    {
        public With(IAbpLazyServiceProvider serviceProvider) => (LazyServiceProvider) = (serviceProvider);

        public IAbpLazyServiceProvider LazyServiceProvider { get; init; }
        public IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.AsyncExecuter();
        public Type ObjectMapperContext { get; set; }
        public IObjectMapper ObjectMapper => LazyServiceProvider.ObjectMapper(ObjectMapperContext);
        public IMediator Mediator => LazyServiceProvider.Mediator();
        public ICachedServiceProvider Cache => LazyServiceProvider.Cache();
        public IUiNotificationService UiNotification => LazyServiceProvider.NotificationService();
        public ILoggerFactory LoggerFactory => LazyServiceProvider.LoggerFactory();

        public T GetRequiredService<T>() => LazyServiceProvider.LazyGetRequiredService<T>();

        #region logging
        public ILogger Logger => LazyServiceProvider.Logger<AbpHandler.With<TQuery>>();
        public void LogTrace(string message, params object[] args) => Logger.LogTrace(message, args);
        public void LogCritical(string message, params object[] args) => Logger.LogCritical(message, args);
        public void LogDebug(string message, params object[] args) => Logger.LogDebug(message, args);
        public void LogError(string message, params object[] args) => Logger.LogError(message, args);
        public void LogException(Exception ex, LogLevel? logLevel = null) => Logger.LogException(ex, logLevel);
        public void LogInformation(string message, params object[] args) => Logger.LogInformation(message, args);
        public void LogWarning(string message, params object[] args) => Logger.LogWarning(message, args);
        public void LogWithLevel(LogLevel logLevel, string message) => Logger.LogWithLevel(logLevel, message);

        #endregion
    }
    public abstract class With<TQuery, TResponse> : Handler.With<TQuery, TResponse>, IBaseHandler
        where TQuery : IQuery<TResponse>
        where TResponse : Response
    {
        protected With(IAbpLazyServiceProvider serviceProvider)
        {
            LazyServiceProvider = serviceProvider;
        }
        public IAbpLazyServiceProvider LazyServiceProvider { get; init; }
        public IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.AsyncExecuter();

        public Type ObjectMapperContext { get; set; }
        public IObjectMapper ObjectMapper => LazyServiceProvider.ObjectMapper(ObjectMapperContext);

        public IMediator Mediator => LazyServiceProvider.Mediator();

        public T GetRequiredService<T>() => LazyServiceProvider.LazyGetRequiredService<T>();

        public ICachedServiceProvider Cache => LazyServiceProvider.Cache();

        public IUiNotificationService UiNotification => LazyServiceProvider.NotificationService();

        #region Logging
        public ILoggerFactory LoggerFactory => LazyServiceProvider.LoggerFactory();

        public ILogger Logger => LazyServiceProvider.Logger<AbpHandler.With<TQuery, TResponse>>();

        public void LogTrace(string message, params object[] args) => Logger.LogTrace(message, args);
        public void LogCritical(string message, params object[] args) => Logger.LogCritical(message, args);
        public void LogDebug(string message, params object[] args) => Logger.LogDebug(message, args);
        public void LogError(string message, params object[] args) => Logger.LogError(message, args);
        public void LogException(Exception ex, LogLevel? logLevel = null) => Logger.LogException(ex, logLevel);
        public void LogInformation(string message, params object[] args) => Logger.LogInformation(message, args);
        public void LogWarning(string message, params object[] args) => Logger.LogWarning(message, args);
        public void LogWithLevel(LogLevel logLevel, string message) => Logger.LogWithLevel(logLevel, message);
        #endregion

        //public abstract Task<Result<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
    }

}