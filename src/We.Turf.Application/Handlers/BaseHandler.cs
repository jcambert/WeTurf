/*
 * using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using We.Results;
namespace We.Turf.Handlers;

public abstract class BaseHandler<TQuery, TResponse> : IRequestHandler<TQuery, IResult< TResponse>>
    where TQuery : IRequest<IResult<TResponse>>
{
    protected BaseHandler(IAbpLazyServiceProvider serviceProvider)
    {
        LazyServiceProvider = serviceProvider;
    }
    protected IAbpLazyServiceProvider LazyServiceProvider { get; init; }
    protected IAsyncQueryableExecuter AsyncExecuter => LazyServiceProvider.LazyGetRequiredService<IAsyncQueryableExecuter>();

    protected Type ObjectMapperContext { get; set; }
    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
        ObjectMapperContext == null
            ? provider.GetRequiredService<IObjectMapper>()
            : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));


    protected IMediator Mediator => LazyServiceProvider.LazyGetRequiredService<IMediator>();

    protected T GetRequiredService<T>() => LazyServiceProvider.LazyGetRequiredService<T>();

    protected ICachedServiceProvider Cache => GetRequiredService<ICachedServiceProvider>();

    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);
    public abstract Task<IResult<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
}
*/
