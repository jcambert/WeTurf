using MediatR;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using We.Results;

namespace We.AbpExtensions;


public abstract class BaseHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IRequest<Result<TResponse>>
    where TResponse : Response
{
    protected BaseHandler(IAbpLazyServiceProvider serviceProvider)
    {
        LazyServiceProvider = serviceProvider;
    }
    protected IAbpLazyServiceProvider LazyServiceProvider { get; init; }
    protected IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.AsyncExecuter();

    protected Type ObjectMapperContext { get; set; }
    protected IObjectMapper ObjectMapper => LazyServiceProvider.ObjectMapper(ObjectMapperContext);

    protected IMediator Mediator => LazyServiceProvider.Mediator();

    protected T GetRequiredService<T>() => LazyServiceProvider.LazyGetRequiredService<T>();

    protected ICachedServiceProvider Cache => LazyServiceProvider.Cache();

    #region Logging
    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LoggerFactory();

    protected ILogger Logger => LazyServiceProvider.Logger<BaseHandler<TQuery,TResponse>>();
    
    protected void LogTrace(string message,params object[] args)=>Logger.LogTrace(message,args);
    protected void LogCritical(string message, params object[] args) => Logger.LogCritical(message, args);
    protected void LogDebug(string message, params object[] args) => Logger.LogDebug(message, args);
    protected void LogError(string message, params object[] args) => Logger.LogError(message, args);
    protected void LogException(Exception ex, LogLevel? logLevel=null) => Logger.LogException(ex, logLevel);
    protected void LogInformation(string message, params object[] args) => Logger.LogInformation(message, args);
    protected void LogWarning(string message, params object[] args) => Logger.LogWarning(message, args);
    protected void LogWithLevel(LogLevel logLevel, string message) => Logger.LogWithLevel(logLevel, message);
    #endregion
   
    public abstract Task<Result<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
}
