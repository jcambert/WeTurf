using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reactive.Linq;
using We.Results;
using We.Utilities;

namespace We.Processes;

public class PythonExecutor : BaseExecutor, IPythonExecutor
{
    public PythonExecutor(PythonExecutorOptions options, params ICommand[] commands) : base(options)
    {
        Commands = commands;
        if (options.UseAnaconda)
            throw new ApplicationException("You cannot use this constructor when using Anaconda\n");
        Configure(options);
    }

    public PythonExecutor(
        PythonExecutorOptions options,
        IAnaconda anaconda,
        params ICommand[] commands
    ) : base(options)
    {
        Commands = commands;

        Configure(options, anaconda);
    }

    public PythonExecutor(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Configure(serviceProvider.GetRequiredService<IOptions<PythonExecutorOptions>>().Value);
    }

    protected virtual void Configure(PythonExecutorOptions options, IAnaconda anaconda = null)
    {
        base.Configure(options);
        if (options.UseAnaconda && !options.ExecuteInConsole)
            ExecuteInConsole = true;
        PythonPath = options.PythonPath.EnsureEndsWith(Path.DirectorySeparatorChar.ToString());
        UseAnaconda = options.UseAnaconda;
        if (UseAnaconda)
            if (ServiceProvider is not null)
            {
                AnacondaActivationCommand =
                    ServiceProvider.GetService<IAnacondaActivationCommand>();
                AnacondaDeactivationCommand =
                    ServiceProvider.GetService<IAnacondaDeactivationCommand>();
            }
            else
            {
                AnacondaActivationCommand = new AnacondaActivationCommand(anaconda);
                AnacondaDeactivationCommand = new AnacondaDeactivationCommand(anaconda);
            }
    }

    public string PythonPath { get; private set; }
    public bool UseAnaconda { get; private set; }

    protected override string GetProcessFilename() =>
        ExecuteInConsole || UseAnaconda ? "cmd.exe" : $"{PythonPath}python.exe";

    protected override string GetArguments(params ICommand[] commands)
    {
        string result = string.Empty;
        if (commands.Count() == 1 && commands[0] is PythonOnlineCommand)
        {
            if (!ExecuteInConsole)
            {
                PythonOnlineCommand cmd = commands[0] as PythonOnlineCommand;
                result = cmd.IsScript ? cmd.Command : $"-c {cmd.Command}";
                commands = new ICommand[0];
            }
            else
            {
                PythonOnlineCommand cmd0 = commands[0] as PythonOnlineCommand;
                string isScript = cmd0.IsScript ? cmd0.Command : @$"-c ""{cmd0.Command}""";
                commands[0] = new PythonOnlineCommand(@$"{PythonPath}python.exe {isScript} ");
            }
        }
        return result;
    }

    protected IAnacondaActivationCommand AnacondaActivationCommand { get; private set; }
    protected IAnacondaDeactivationCommand AnacondaDeactivationCommand { get; private set; }

    protected override Action<StreamWriter> GetBeforeSendCommands() =>
        (StreamWriter sw) => AnacondaActivationCommand?.Send(sw);

    protected override Action<StreamWriter> GetAfterSendCommands() =>
        (StreamWriter sw) => AnacondaDeactivationCommand?.Send(sw);

    protected override Func<ICommand, bool> GetFilterCommand() =>
        (ICommand command) => !(!UseAnaconda && command is IAnacondaCommand);

    /* private async Task<Result> ExecuteWithoutReactive(ProcessStartInfo psi, CancellationToken cancellationToken, params ICommand[] commands)
     {
         StringBuilder result = new StringBuilder();
         var proc = new Process()
         {
             StartInfo = psi
         };
         Logger?.LogInformation("Starting Python Script Executor Process");

         if (proc.Start())
         {
             var _commands = new List<ICommand>(Commands);
             _commands.AddRange(commands);

             using (var sw = proc.StandardInput)
             {
                 if (sw.BaseStream.CanWrite)
                 {
                     AnacondaActivationCommand?.Send(sw);
                     foreach (var command in _commands)
                     {
                         if (!UseAnaconda && command is IAnacondaCommand)
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
     }*/
    /*  protected override async Task<Result> ExecuteWithReactive(ProcessStartInfo psi, CancellationToken cancellationToken, params ICommand[] commands)
      {
          CancellationTokenSource stoppingTokenSource = new CancellationTokenSource();
          var stoppingToken = stoppingTokenSource.Token;

          var proc = new Process()
          {
              StartInfo = psi
          };
          Logger?.LogInformation("Starting Python Script Executor Process");

          if (proc.Start())
          {
              Logger?.LogInformation("Python Script Executor Process Started");
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
              var _commands = new List<ICommand>(Commands);
              _commands.AddRange(commands);

              using (var sw = proc.StandardInput)
              {
                  if (sw.BaseStream.CanWrite)
                  {
                      AnacondaActivationCommand?.Send(sw);
                      foreach (var command in _commands)
                      {
                          if (!UseAnaconda && command is IAnacondaCommand)
                              continue;
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
              Logger?.LogInformation("Python Script Executor ending normaly");
          }
          else
          {
              Logger?.LogInformation("Python Script Executor Process cannot be started");
          }
          return Result.Success();
      }*/


    /*public override async Task<Result> Execute(CancellationToken cancellationToken, params ICommand[] commands)
    {
        CancellationTokenSource stoppingTokenSource = new CancellationTokenSource();
        var stoppingToken = stoppingTokenSource.Token;
        string filename = ExecuteInConsole || UseAnaconda ? "cmd.exe" : $"{PythonPath}python.exe";
        string args = string.Empty;
        if (commands.Count() == 1 && commands[0] is PythonOnlineCommand)
        {
            if (!ExecuteInConsole)
            {
                PythonOnlineCommand cmd = commands[0] as PythonOnlineCommand;
                args = cmd.IsScript ? cmd.Command : $"-c {cmd.Command}";
                commands = new ICommand[0];
            }
            else
            {
                PythonOnlineCommand cmd0 = commands[0] as PythonOnlineCommand;
                string isScript = cmd0.IsScript ? cmd0.Command : @$"-c ""{cmd0.Command}""";
                commands[0] = new PythonOnlineCommand(@$"{PythonPath}python.exe {isScript} ");
            }
        }

        var psi = CreateProcessStartInfo(filename, args, WorkingDirectory);
        Func<ProcessStartInfo, CancellationToken, ICommand[], Task<Result>> fn = UseReactiveOutput ? ExecuteWithReactive : ExecuteWithoutReactive;
        return await fn(psi, cancellationToken, commands);
    }*/

    public override Task<Result> SendAsync(string cmd, CancellationToken cancellationToken)
    {
        var command = new PythonOnlineCommand(cmd);
        return Execute(cancellationToken, command);
    }
}
