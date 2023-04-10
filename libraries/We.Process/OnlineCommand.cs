namespace We.Processes;

public  class OnlineCommand : BaseCommand
{
    public OnlineCommand(string cmd)
    {
        this.Command = cmd;
    }
    public string Command { get; }

    public override string GetCommand() => Command;
}