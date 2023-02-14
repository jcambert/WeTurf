namespace We.Turf.Service;

public abstract class BaseCommand : ICommand
{
    public BaseCommand(IServiceProvider serviceProvider)
        =>(ServiceProvider, Logger,ExecutingDriveLetter) = 
        (serviceProvider, 
        serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(GetType().FullName),
        serviceProvider.GetRequiredService<DriveService>().ExecutingDriveLetter
        );
    protected ILogger Logger { get; init; }
    protected IServiceProvider ServiceProvider { get; init; }
    protected string ExecutingDriveLetter { get; }

    protected virtual IEnumerable<string> Commands()
    {
        yield break;
    }
    protected void InnerWrite(Action<string> writer)
    {
        if (writer == null)
            return;
        foreach (var cmd in Commands())
        {
            writer(cmd);

        }
    }


    public virtual void Send(TextWriter writer) => InnerWrite(writer.WriteLine);

    public virtual void Send(IObserver<string> commandpython) => InnerWrite(commandpython.OnNext);
}
