namespace We.Processes;

public interface ICommand
{
    void Send(TextWriter writer);
    Task SendAsync(TextWriter writer);
    string GetCommand();
}
