namespace We.Processes;

public abstract class BaseCommand : ICommand
{
    public abstract string GetCommand();

    public virtual void Send(TextWriter writer) => writer.WriteLine(GetCommand());

    public virtual Task SendAsync(TextWriter writer) => writer.WriteLineAsync(GetCommand());
}
