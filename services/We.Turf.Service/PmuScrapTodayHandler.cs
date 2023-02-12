using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace We.Turf.Service;
public interface IWinCommand : IObserver<string>, IDisposable
{
    //void Start();
    //Task ExecuteAsync(CancellationToken cancellationToken, params string[] args);
    //void End();
    void Initialize();
    IObservable<string> OnOutput { get; }
}
public class WinCommand : IWinCommand
{
    private ISubject<string> _onOutput = new Subject<string>();
    public IObservable<string> OnOutput => _onOutput.AsObservable();

    public string WorkingDirectory { get; set; }
    protected IDisposable Output { get; private set; }
    protected StreamWriter Input { get; private set; }
    protected StreamReader Error { get; private set; }
    protected Process Process { get; private set; }
    protected IServiceProvider Services { get; init; }

    public WinCommand(IServiceProvider services)
    {
        this.Services = services;
    }

    public void Dispose()
    {
        Output?.Dispose();
        Input?.Dispose();
        Error?.Dispose();
        Process?.Dispose();
    }

    protected virtual void End()
    {
        Input?.Close();
        Error?.Close();
        Process?.Close();
    }

    public void OnCompleted()
    {
        End();
    }

    public void OnError(Exception error)
    {

    }

    public void OnNext(string value)
    {
        if (Input.BaseStream.CanWrite)
        {
            Input.WriteLine(value);
        }
    }

    public virtual void Initialize()
    {
        if (Process != null  )
        {
            throw new ApplicationException("Process allready started");
        }
        var psi = new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            WorkingDirectory = WorkingDirectory
        };

        Process = new Process()
        {
            StartInfo = psi
        };
        
        if (Process.Start())
        {

            var i = Observable.Using(
                () => Process.StandardOutput,
                reader => Observable.FromAsync(reader.ReadLineAsync)
                    .Repeat()
                    .TakeWhile(x => x != null)
                );
            Output = i.Subscribe(
                 x => {
                     _onOutput.OnNext(x);

                 },
                 () =>
                 {
                     _onOutput.OnCompleted();
                 });

            Input = Process.StandardInput;
            //Error = Process.StandardError;

           
        }
    }
}
public class ActivateAnaconda : BasePipeline<PmuScrapTodayQuery, PmuScrapTodayResponse>
{
    public ActivateAnaconda(IServiceProvider services) : base(services)
    {
        
    }

    

    public override ValueTask<PmuScrapTodayResponse> Handle(PmuScrapTodayQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<PmuScrapTodayQuery, PmuScrapTodayResponse> next)
    {
        Logger?.LogTrace("Start Anaconda Activation");
        var anacondaActivator = ServiceProvider.GetRequiredService<IAnacondaActivation>();
        anacondaActivator.Send(Python);
        Logger?.LogTrace("End Anaconda Activation");
        return next(message, cancellationToken);
    }
}
public class PmuScrapToday : BasePipeline<PmuScrapTodayQuery, PmuScrapTodayResponse>
{
    public PmuScrapToday(IServiceProvider services) : base(services)
    {
    }

    public override ValueTask<PmuScrapTodayResponse> Handle(PmuScrapTodayQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<PmuScrapTodayQuery, PmuScrapTodayResponse> next)
    {
        Logger?.LogTrace("Start Pmu Scrapper Today");
        var scrapper = ServiceProvider.GetRequiredService<PmuScrapTodayScript>();
        scrapper.Send(Python);
        Logger?.LogTrace("End Pmu Scrapper Today");
        return next(message, cancellationToken);
    }
}

public class PmuScrapTodayHandler : BaseRequestHandler<PmuScrapTodayQuery, PmuScrapTodayResponse>
{
    public PmuScrapTodayHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override  ValueTask<PmuScrapTodayResponse> Handle(PmuScrapTodayQuery request, CancellationToken cancellationToken)
    {
        
        return ValueTask.FromResult(new PmuScrapTodayResponse());
    }
}

public class PmuPredictToday : BasePipeline<PmuPredictTodayQuery, PmuPredictTodayResponse>
{
    public PmuPredictToday(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async ValueTask<PmuPredictTodayResponse> Handle(PmuPredictTodayQuery message, CancellationToken cancellationToken, MessageHandlerDelegate<PmuPredictTodayQuery, PmuPredictTodayResponse> next)
    {
        await Task.Delay(100);
        Logger?.LogTrace("Start Pmu Predicter Today");
        var predicter=ServiceProvider.GetRequiredService<PmuPredictTodayScript>();
        predicter.Send(Python);
        Logger?.LogTrace("End Pmu Predicter Today");
        return new PmuPredictTodayResponse();
        
    }
}

public class PmuPredictTodayHandler : BaseRequestHandler<PmuPredictTodayQuery, PmuPredictTodayResponse>
{
    public PmuPredictTodayHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {

    }

    public override ValueTask<PmuPredictTodayResponse> Handle(PmuPredictTodayQuery request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult( new PmuPredictTodayResponse());
    }
}