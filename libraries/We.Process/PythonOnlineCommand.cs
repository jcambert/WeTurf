namespace We.Processes;

internal sealed class PythonOnlineCommand : OnlineCommand
{
    internal PythonOnlineCommand(string cmd) : base(cmd)
    {
        var v = cmd.Split(" ");
        IsScript = v.Length > 0 && v[0].ToLower().EndsWith(".py");
    }

    public bool IsScript { get; }
}
