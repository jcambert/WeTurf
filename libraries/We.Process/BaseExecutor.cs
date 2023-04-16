using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using We.Results;
namespace We.Processes;

public abstract class BaseExecutor : IExecutor
{
    public BaseExecutor(ExecutorOptions options)
    {
        Scheduler = new EventLoopScheduler();
        Configure(options);
    }
    public BaseExecutor(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        OutputPipe = serviceProvider.GetService<IReactiveOutputExecutor>();
        Commands = ServiceProvider.GetServices<ICommand>();
        Logger = ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(this.GetType());
        var o = serviceProvider.GetRequiredService<IOptions<ExecutorOptions>>().Value;

        Scheduler = serviceProvider.GetRequiredService<ISchedulerProvider>().Scheduler;
        Configure(o);
    }
    protected virtual void Configure(ExecutorOptions options)
    {
        if (!string.IsNullOrEmpty(options.WorkingDirectory))
            WorkingDirectory = options.WorkingDirectory;
        UseReactiveOutput = options.UseReactiveOutput;
        ExecuteInConsole = options.ExecuteInConsole;
        if (UseReactiveOutput && OutputPipe != null)
            OutputPipe.Connect(this);

    }

    protected IEnumerable<ICommand> Commands { get; init; }
    public string WorkingDirectory { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    protected ISubject<string> _output = new Subject<string>();
    public IObservable<string> OnOutput => _output.AsObservable();
    protected IServiceProvider ServiceProvider { get; }
    public IReactiveOutputExecutor OutputPipe { get; }
    protected ILogger Logger { get; }
    public bool UseReactiveOutput { get; protected set; }
    public bool ExecuteInConsole { get; protected set; }
    public IScheduler Scheduler { get; init; }

    protected ProcessStartInfo CreateProcessStartInfo(string filename, string args, string workingDirectory)
    => new ProcessStartInfo()
    {
        FileName = filename,
        RedirectStandardInput = true,
        UseShellExecute = false,
        RedirectStandardOutput = true,
        WorkingDirectory = workingDirectory,
        Arguments = args

    };

    protected virtual string GetProcessFilename()
        => "cmd.exe";

    protected virtual string GetArguments(params ICommand[] commands)
        => string.Empty;

    protected virtual Func<ICommand, bool> GetFilterCommand() => null;
    protected virtual Action<StreamWriter> GetBeforeSendCommands() => null;
    protected virtual Action<StreamWriter> GetAfterSendCommands() => null;
    public virtual Task<Result> Execute(
        CancellationToken cancellationToken = default,
        params ICommand[] commands)
    {
        CancellationTokenSource stoppingTokenSource = new CancellationTokenSource();
        var stoppingToken = stoppingTokenSource.Token;
        string filename = GetProcessFilename();
        string args = GetArguments(commands);


        var psi = CreateProcessStartInfo(filename, args, WorkingDirectory);
        Func<ProcessStartInfo, CancellationToken, Func<ICommand, bool>, Action<StreamWriter>, Action<StreamWriter>, ICommand[], Task<Result>> fn =
            UseReactiveOutput ? ExecuteWithReactive : ExecuteWithoutReactive;
        return fn(psi, cancellationToken, GetFilterCommand(), GetBeforeSendCommands(), GetAfterSendCommands(), commands);
    }

    protected virtual async Task<Result> ExecuteWithoutReactive(
        ProcessStartInfo psi,
        CancellationToken cancellationToken,
        Func<ICommand, bool> filterCommand = null,
        Action<StreamWriter> beforeSendCommands = null,
        Action<StreamWriter> afterSendCommands = null,
        params ICommand[] commands)
    {
        StringBuilder result = new StringBuilder();
        var proc = new Process()
        {
            StartInfo = psi
        };
        Logger?.LogInformation("Starting Python Script Executor Process");

        if (proc.Start())
        {
            var _commands = new List<ICommand>(Commands ?? new ICommand[0]);
            _commands.AddRange(commands);

            using (var sw = proc.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    if (beforeSendCommands is not null)
                        beforeSendCommands(sw);
                    //AnacondaActivationCommand?.Send(sw);
                    foreach (var command in _commands)
                    {

                        if (filterCommand is not null && !filterCommand(command))
                            continue;
                        await command.SendAsync(sw);
                    }
                    if (afterSendCommands is not null)
                        afterSendCommands(sw);
                }
            }
            using (StreamReader reader = proc.StandardOutput)
            {
                var res = await reader.ReadToEndAsync(cancellationToken);
                result.AppendLine(res);
            }

        }
        return Result.Success<string>(result.ToString());
    }
    protected virtual async Task<Result> ExecuteWithReactive(
        ProcessStartInfo psi,
        CancellationToken cancellationToken,
        Func<ICommand, bool> filterCommand = null,
        Action<StreamWriter> beforeSendCommands = null,
        Action<StreamWriter> afterSendCommands = null,
        params ICommand[] commands)
    {
        CancellationTokenSource stoppingTokenSource = new CancellationTokenSource();
        var stoppingToken = stoppingTokenSource.Token;
        
        var proc = new Process()
        {
            StartInfo = psi
        };
        Logger?.LogInformation("Starting Executor Process");

        if (proc.Start())
        {
            var scheduler = new EventLoopScheduler();
            Logger?.LogInformation("Executor Process Started");

           /* var i=Observable.Generate(
                    proc.StandardOutput,
                    reader => !reader.EndOfStream,
                    reader => reader,
                    reader => reader.ReadLine()
                );*/
            var j = Observable.Using(
               () => proc.StandardOutput,
               reader => Observable.FromAsync(reader.ReadLineAsync, Scheduler)
                   .Repeat()
                   .TakeWhile(x => x != null)

               ).ObserveOn(Scheduler);
           j.SubscribeOn(Scheduler).Subscribe(
               x => _output.OnNext(x),
               () =>
               {
                   stoppingTokenSource.Cancel();
               });
            //copy Commands that might be in ServiceCollection
            //and add to it function passed arguments
            var _commands = new List<ICommand>(Commands);
            _commands.AddRange(commands);

            using (var sw = proc.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    if (beforeSendCommands is not null)
                        beforeSendCommands(sw);
                    //AnacondaActivationCommand?.Send(sw);
                    foreach (var command in _commands)
                    {
                        if (filterCommand is not null && !filterCommand(command))
                            continue;
                        // if (!UseAnaconda && command is IAnacondaCommand)
                        //   continue;
                        await command.SendAsync(sw);
                    }
                    if (afterSendCommands is not null)
                        afterSendCommands(sw);
                }
            }


            while (!stoppingToken.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500, cancellationToken);
                if (stoppingToken.IsCancellationRequested || cancellationToken.IsCancellationRequested)
                {
                    _output.OnCompleted();
                }

            }
            Logger?.LogInformation("Script Executor ending normaly");
        }
        else
        {
            Logger?.LogInformation("Script Executor Process cannot be started");
        }
        return Result.Success();
    }

    public virtual Task<Result> SendAsync(string cmd, CancellationToken cancellationToken = default)
    {
        var command = new OnlineCommand(cmd);
        return Execute(cancellationToken, command);
    }
}
