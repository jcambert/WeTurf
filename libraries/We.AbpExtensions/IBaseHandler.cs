using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using We.Mediatr;
using We.Results;
#if MEDIATR
using MediatR;
#endif
#if MEDIATOR
using Mediator;
#endif
namespace We.AbpExtensions;

public interface IBaseHandler
{
    IAbpLazyServiceProvider LazyServiceProvider { get; }
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

    void LogTrace(string message, params object[] args);
    void LogCritical(string message, params object[] args);
    void LogDebug(string message, params object[] args);
    void LogError(string message, params object[] args);
    void LogException(Exception ex, LogLevel? logLevel = null);
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogWithLevel(LogLevel logLevel, string message);

    #endregion


}

public static class AbpHandler
{
    public abstract class With<TQuery> : Handler.With<TQuery>, IBaseHandler where TQuery : IQuery
    {
        public With(IAbpLazyServiceProvider serviceProvider) =>
            (LazyServiceProvider) = (serviceProvider);

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

        public void LogTrace(string message, params object[] args) =>
            Logger.LogTrace(message, args);

        public void LogCritical(string message, params object[] args) =>
            Logger.LogCritical(message, args);

        public void LogDebug(string message, params object[] args) =>
            Logger.LogDebug(message, args);

        public void LogError(string message, params object[] args) =>
            Logger.LogError(message, args);

        public void LogException(Exception ex, LogLevel? logLevel = null) =>
            Logger.LogException(ex, logLevel);

        public void LogInformation(string message, params object[] args) =>
            Logger.LogInformation(message, args);

        public void LogWarning(string message, params object[] args) =>
            Logger.LogWarning(message, args);

        public void LogWithLevel(LogLevel logLevel, string message) =>
            Logger.LogWithLevel(logLevel, message);

        #endregion

        #region Result

        #endregion
    }

    public abstract class With<TQuery, TResponse> : Handler.With<TQuery, TResponse>, IBaseHandler
        where TQuery : We.Mediatr.IQuery<TResponse>
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

        public void LogTrace(string message, params object[] args) =>
            Logger.LogTrace(message, args);

        public void LogCritical(string message, params object[] args) =>
            Logger.LogCritical(message, args);

        public void LogDebug(string message, params object[] args) =>
            Logger.LogDebug(message, args);

        public void LogError(string message, params object[] args) =>
            Logger.LogError(message, args);

        public void LogException(Exception ex, LogLevel? logLevel = null) =>
            Logger.LogException(ex, logLevel);

        public void LogInformation(string message, params object[] args) =>
            Logger.LogInformation(message, args);

        public void LogWarning(string message, params object[] args) =>
            Logger.LogWarning(message, args);

        public void LogWithLevel(LogLevel logLevel, string message) =>
            Logger.LogWithLevel(logLevel, message);
        #endregion

        #region Result
        protected Result<TResponse> NotFound(string failure, string message = null) =>
            Result.Failure<TResponse>(HttpStatusCode.NOT_FOUND, failure, message);
        #endregion
    }

    public abstract class With<TQuery, TResponse, TEntity, TEntityDto> : With<TQuery, TResponse>
        where TQuery : We.Mediatr.IQuery<TResponse>
        where TResponse : Response
        where TEntity : Entity
        where TEntityDto : EntityDto
    {
        protected IRepository<TEntity> Repository => GetRequiredService<IRepository<TEntity>>();

        protected With(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

        protected List<TEntityDto> MapToDtoList(List<TEntity> entities) =>
            ObjectMapper.Map<List<TEntity>, List<TEntityDto>>(entities);

        protected TEntityDto MapToDto(TEntity entity) =>
            ObjectMapper.Map<TEntity, TEntityDto>(entity);
    }

    public abstract class With<TQuery, TResponse, TEntity, TEntityDto, TKey>
        : With<TQuery, TResponse, TEntity, TEntityDto>
        where TQuery : We.Mediatr.IQuery<TResponse>
        where TResponse : Response
        where TEntity : Entity<TKey>
        where TEntityDto : EntityDto<TKey>
    {
        protected With(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
