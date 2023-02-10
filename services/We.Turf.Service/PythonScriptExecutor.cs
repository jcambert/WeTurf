using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace We.Turf.Service;

internal class PythonScriptExecutor<TCommand> : IExecutor<TCommand>
    where TCommand : ICommand
{
    protected IEnumerable<TCommand> Commands { get; init; }
    protected ILogger<PythonScriptExecutor<TCommand>> Logger { get; init; }
    protected IServiceProvider ServiceProvider { get; init; }

    protected IAnacondaActivation AnacondaActivation { get; init; }
    protected bool UsingAnaconda { get; init; }
    public PythonScriptExecutor(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<PythonScriptExecutor<TCommand>>();
        AnacondaActivation= serviceProvider.GetService<IAnacondaActivation>();
        UsingAnaconda = AnacondaActivation != null;
        Commands = ServiceProvider.GetServices<TCommand>();
    }
    public string WorkingDirectory { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    private ISubject<string> _onOutput = new Subject<string>();
    public IObservable<string> OnOutput => _onOutput.AsObservable();
    public async Task Execute(CancellationToken stoppingToken)
    {
        bool _isRunning = false;
        var psi = new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            WorkingDirectory = WorkingDirectory
        };

        var proc = new Process()
        {
            StartInfo = psi
        };

        Logger.LogInformation("Starting Python Script Executor Process");
        if (proc.Start())
        {
            _isRunning = true;
            Logger.LogInformation("Python Script Executor Process Started");
            var i = Observable.Using(
                () => proc.StandardOutput,
                reader => Observable.FromAsync(reader.ReadLineAsync)
                    .Repeat()
                    .TakeWhile(x => x != null)
                );
            i.Subscribe(
                x => _onOutput.OnNext(x),
                () =>
                {
                    _onOutput.OnCompleted();
                    _isRunning = false;
                });

            // Pass multiple commands to cmd.exe
            using (var sw = proc.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    AnacondaActivation?.Send(sw);
                    foreach (var command in Commands)
                    {
                        if (!UsingAnaconda && command is IAnacondaCommand)
                            continue;
                        command.Send(sw);
                    }
                }
            }


            while (_isRunning)
            {
                await Task.Delay(500, stoppingToken);
                if (stoppingToken.IsCancellationRequested)
                {
                    _isRunning = false;
                    _onOutput.OnCompleted();
                }

            }
            Logger.LogInformation("Python Script Executor ending normaly");
        }
        else
        {
            Logger.LogInformation("Python Script Executor Process cannot be started");
        }
        return;

    }
}
