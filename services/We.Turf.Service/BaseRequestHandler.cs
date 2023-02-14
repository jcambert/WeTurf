namespace We.Turf.Service;

public abstract class BaseRequestHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IRequest<TResponse>
{
    protected IServiceProvider ServiceProvider { get; init; }
    protected ILogger Logger { get; init; }
    public BaseRequestHandler(IServiceProvider serviceProvider) => (ServiceProvider,Logger)=(serviceProvider,serviceProvider.GetService<ILoggerFactory>().CreateLogger(GetType().FullName));


    public abstract ValueTask<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
}

public abstract class BaseCommandRequestHandler<TQuery, TResponse> : BaseRequestHandler<TQuery, TResponse>
    where TQuery : IRequest<TResponse>
{
    protected IWinCommand Python { get; init; }
    public BaseCommandRequestHandler(IServiceProvider serviceProvider):base(serviceProvider) => ( Python) = ( serviceProvider.GetRequiredService<IWinCommand>());


}