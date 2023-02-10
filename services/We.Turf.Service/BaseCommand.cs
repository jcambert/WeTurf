namespace We.Turf.Service;

public abstract class BaseCommand : ICommand
{
    public BaseCommand(IServiceProvider serviceProvider)
        =>(ServiceProvider, Logger,ExecutingDriveLetter) = 
        (serviceProvider, 
        serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<AnacondaCommand>(),
        serviceProvider.GetRequiredService<DriveService>().ExecutingDriveLetter
        );
    protected ILogger<AnacondaCommand> Logger { get; init; }
    protected IServiceProvider ServiceProvider { get; init; }
    protected string ExecutingDriveLetter { get; }

    public abstract void Send(TextWriter writer);
}
