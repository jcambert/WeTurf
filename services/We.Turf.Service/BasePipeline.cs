namespace We.Turf.Service;

public abstract class BasePipeline<TQuery, TResponse> : IPipelineBehavior<TQuery, TResponse>
    where TQuery : IRequest<TResponse>
{
    public BasePipeline(IServiceProvider services)
    {
        this.ServiceProvider = services;
        this.Python = ServiceProvider.GetRequiredService<IWinCommand>();
        this.Logger= services.GetService<ILogger>();
    }

    protected IServiceProvider ServiceProvider { get; init; }
    protected IWinCommand Python { get; init; }
    protected ILogger Logger { get; init; }

    public abstract ValueTask<TResponse> Handle(TQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<TQuery, TResponse> next);
}
