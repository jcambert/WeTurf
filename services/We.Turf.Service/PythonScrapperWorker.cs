using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace We.Turf.Service;

public class PmuScrapAndPredictTodayWorker : BackgroundService
{
    protected ILogger<PmuScrapAndPredictTodayWorker> Logger { get; init; }
    
    private IServiceProvider ServiceProvider { get; init; }
    public PmuScrapAndPredictTodayWorker(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        this.Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<PmuScrapAndPredictTodayWorker>();
        
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scrapper = ServiceProvider.GetRequiredService<IExecutor>();
        scrapper.OnOutput.Subscribe(x => Logger.LogInformation(x));
        await scrapper.Execute(stoppingToken);
     
        //while (!stoppingToken.IsCancellationRequested)
        //{
        //Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //await Task.Delay(1000, stoppingToken);
        //}
    }
}

public interface ICommand
{
    void Send(TextWriter writer);
}

public abstract class BaseCommand : ICommand
{
    public BaseCommand(IServiceProvider serviceProvider)
        =>(ServiceProvider, Logger) = (serviceProvider, serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<AnacondaCommand>());
    protected ILogger<AnacondaCommand> Logger { get; init; }
    protected IServiceProvider ServiceProvider { get; init; }

    public abstract void Send(TextWriter writer);
}

public abstract class AnacondaCommand : BaseCommand
{
    protected IAnaconda Conda { get; init; }
    protected AnacondaCommand(IServiceProvider serviceProvider):base(serviceProvider) =>
        ( Conda) = (serviceProvider.GetRequiredService<IAnaconda>());

}
public class AnacondaActivation : AnacondaCommand
{

    public AnacondaActivation(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override void Send(TextWriter writer)
    { // Vital to activate Anaconda
        writer.WriteLine($@"{Conda.BasePath}\Scripts\activate.bat");

        // Activate your environment
        writer.WriteLine($"activate {Conda.EnvironmentName}");

    }
}
public abstract class AnacondaExecuteStript : AnacondaCommand
{
    public string ScriptName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Arguments { get; set; } = string.Empty;

    public AnacondaExecuteStript(IServiceProvider serviceProvider) : base(serviceProvider) { }
    public override void Send(TextWriter writer)
    {
        var s = $@"{Conda.BasePath}\python.exe {ScriptArguments()}";
        Logger.LogDebug($"Executing Ananconda Script \n{s}");
        writer.WriteLine(s);
    }

    protected virtual string ScriptArguments() => $@"{Path}\{ScriptName}.py {Arguments}";
}

public class PmuScrapToDay : AnacondaExecuteStript
{
    public PmuScrapToDay(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "__init__";
        Path = @"D:\projets\pmu_scrapper";
    }

}

public class PmuPredictToDay : AnacondaExecuteStript
{
    public PmuPredictToDay(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ScriptName = "predicter";
        Path = @"D:\projets\pmu_scrapper";
    }

}

public class PmuScrapAndPredictTodayEnded : BaseCommand
{
    public PmuScrapAndPredictTodayEnded(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override void Send(TextWriter writer)
    {
        
    }
}
public interface IAnaconda
{
    string BasePath { get; }
    string EnvironmentName { get; }
}
public class Anaconda : IAnaconda
{
    public string EnvironmentName { get; set; } = "base";
    public string BasePath { get; set; } = @"D:\anaconda3";
}
public interface IExecutor
{
    IObservable<string> OnOutput { get; }
    Task Execute(CancellationToken stoppingToken);
}


internal class PythonScriptExecutor : IExecutor
{
    protected IEnumerable<ICommand> Commands { get; init; }
    protected ILogger<PythonScriptExecutor> Logger { get; init; }
    protected IServiceProvider ServiceProvider { get; init; }
    public PythonScriptExecutor(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<PythonScriptExecutor>();
        Commands = ServiceProvider.GetServices<ICommand>();
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
                    foreach (var command in Commands)
                    {
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
