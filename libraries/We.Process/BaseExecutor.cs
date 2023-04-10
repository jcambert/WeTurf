using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
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
        Configure(options);
    }
    public BaseExecutor(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        Commands = ServiceProvider.GetServices<ICommand>();
        Logger = ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(this.GetType());
        var o = serviceProvider.GetRequiredService<IOptions<ExecutorOptions>>().Value;
        Configure(o);
    }
    protected virtual void Configure(ExecutorOptions options)
    {
        if (!string.IsNullOrEmpty(options.WorkingDirectory))
            WorkingDirectory = options.WorkingDirectory;
        UseReactiveOutput = options.UseReactiveOutput;
       
        ExecuteInConsole = options.ExecuteInConsole;
        
    }

    protected IEnumerable<ICommand> Commands { get; init; }
    public string WorkingDirectory { get; set; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    protected ISubject<string> _output = new Subject<string>();
    public IObservable<string> OnOutput => _output.AsObservable();
    protected IServiceProvider ServiceProvider { get; }
    protected ILogger Logger { get; }
    public bool UseReactiveOutput { get; private set; }
    public bool ExecuteInConsole { get; private set; }
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
    public virtual Task<Result> Execute(CancellationToken cancellationToken=default, params ICommand[] commands)
    {
        CancellationTokenSource stoppingTokenSource = new CancellationTokenSource();
        var stoppingToken = stoppingTokenSource.Token;
        string filename = GetProcessFilename();
        string args =GetArguments(commands);
        

        var psi = CreateProcessStartInfo(filename, args, WorkingDirectory);
        Func<ProcessStartInfo, CancellationToken, Func<ICommand, bool>,Action<StreamWriter>, ICommand[], Task<Result>> fn =
            UseReactiveOutput ? ExecuteWithReactive : ExecuteWithoutReactive;
        return  fn(psi, cancellationToken,GetFilterCommand(),GetBeforeSendCommands(), commands);
    }

    protected virtual async Task<Result> ExecuteWithoutReactive(ProcessStartInfo psi,
        CancellationToken cancellationToken,
        Func<ICommand, bool> filterCommand = null,
        Action<StreamWriter> beforeSendCommands = null,
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
                }
            }
            using (StreamReader reader = proc.StandardOutput)
            {
                var res = await reader.ReadToEndAsync();
                result.AppendLine(res);
            }

        }
        return Result.Sucess<string>(result.ToString());
    }
    protected virtual async Task<Result> ExecuteWithReactive(
        ProcessStartInfo psi, 
        CancellationToken cancellationToken, 
        Func<ICommand,bool> filterCommand=null,
        Action<StreamWriter> beforeSendCommands=null,
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
            Logger?.LogInformation("Executor Process Started");
            var i = Observable.Using(
                () => proc.StandardOutput,
                reader => Observable.FromAsync(reader.ReadLineAsync)
                    .Repeat()
                    .TakeWhile(x => x != null)
                );
            i.Subscribe(
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
