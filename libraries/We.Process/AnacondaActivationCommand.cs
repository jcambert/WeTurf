namespace We.Processes;

public class AnacondaActivationCommand : AnacondaCommand, IAnacondaActivationCommand
{
    public AnacondaActivationCommand(IAnaconda conda) : base(conda)
    {
    }

    public AnacondaActivationCommand(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override string GetCommand()
    {
        throw new NotImplementedException();
    }

    public override void Send(TextWriter writer)
    {
        // Vital to activate Anaconda
        writer.WriteLine($@"{Conda.BasePath}\Scripts\activate.bat");

        // Activate your environment
        writer.WriteLine($"activate {Conda.EnvironmentName}");
    }

    public override async Task SendAsync(TextWriter writer)
    {
        // Vital to activate Anaconda
        await writer.WriteLineAsync($@"{Conda.BasePath}\Scripts\activate.bat");

        // Activate your environment
        await writer.WriteLineAsync($"activate {Conda.EnvironmentName}");
    }
}
