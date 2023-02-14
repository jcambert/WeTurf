using System;
using System.Diagnostics;
using System.Reactive.Subjects;

namespace We.Turf.Service;

public class WinCommand : IWinCommand
{
    private ILogger Logger { get; init; }
    private ISubject<string> _onOutput = new Subject<string>();
    public IObservable<string> OnOutput => _onOutput.AsObservable();

    public string WorkingDirectory { get; set; }
    protected IDisposable Output { get; private set; }
    protected StreamWriter Input { get; private set; }
    protected StreamReader Error { get; private set; }
    protected Process Process { get; private set; }
    protected IServiceProvider Services { get; init; }
    private readonly Guid Id= Guid.NewGuid();
    public WinCommand(IServiceProvider services)
    {
        this.Services = services;
        Logger=services.GetRequiredService<ILoggerFactory>().CreateLogger(GetType().FullName);
        Logger.LogInformation($"Create New {nameof(WinCommand)} with Id:{Id}");
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
        
        _onOutput.OnCompleted();
        End();
    }

    public void OnError(Exception error)
    {
        _onOutput.OnError(error);
        
    }

    public void OnNext(string value)
    {
        if(Process== null) 
            Initialize();
        if (Input.BaseStream.CanWrite)
        {
            Input.WriteLine(value);
        }
        
    }

    protected virtual void Initialize()
    {
        if (Process != null  )
        {
            throw new ApplicationException("Process allready started");
        }
        Logger.LogInformation($"Initialize  {nameof(WinCommand)} with Id:{Id}");
        var psi = new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = WorkingDirectory,
            
            
        };

        Process = new Process()
        {
            StartInfo = psi,
            EnableRaisingEvents= true,
            
        };
        
        if (Process.Start())
        {
            

            var i = Observable.Using(
                () => Process.StandardOutput,
                reader => Observable.FromAsync(reader.ReadToEndAsync)
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

           

        }
       
    }
}
