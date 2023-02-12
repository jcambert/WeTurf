namespace We.Turf.Service;

public abstract class BaseRequestHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IRequest<TResponse>
{
    protected IServiceProvider ServiceProvider { get; init; }
    protected IWinCommand Python { get; init; }
    protected ILogger Logger { get; init; }
    public BaseRequestHandler(IServiceProvider serviceProvider) => (ServiceProvider,Python,Logger)=(serviceProvider, serviceProvider.GetRequiredService<IWinCommand>(),serviceProvider.GetService<ILogger>());


    public abstract ValueTask<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
}
