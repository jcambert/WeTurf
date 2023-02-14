using Microsoft.Extensions.Options;

namespace We.Turf.Service;


public abstract class BasePipeline<TQuery, TResponse> : IPipelineBehavior<TQuery, TResponse>
    where TQuery : IRequest<TResponse>
{
    public BasePipeline(IServiceProvider services)
    {
        this.ServiceProvider = services;
       // this.Python = ServiceProvider.GetRequiredService<IWinCommand>() ;
        this.Logger= services.GetService<ILoggerFactory>().CreateLogger(GetType().FullName);
    }

    protected virtual IServiceProvider ServiceProvider { get;  }
    protected ILogger Logger { get; init; }

    public abstract ValueTask<TResponse> Handle(TQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<TQuery, TResponse> next);

}

public abstract class BasePythonConsolePipeline<TQuery, TResponse> : BasePipeline<TQuery, TResponse>,IDisposable
    where TQuery : IRequest<TResponse>
{
    private IDisposable _pythonConsoleListener;
    public bool IsProcessing { get; protected set; }
    protected IWinCommand Python { get;  private set; }
    protected BasePythonConsolePipeline(IServiceProvider services) : base(services)
    {
        Python = CreatePythonConsole();
    }
    protected virtual IWinCommand CreatePythonConsole()
    {
        var python = ServiceProvider.GetRequiredService<IWinCommand>();
        
            
        
        //python.Initialize();
        return python;
    }

    public void Dispose()
    {
        _pythonConsoleListener?.Dispose();
    }
}
