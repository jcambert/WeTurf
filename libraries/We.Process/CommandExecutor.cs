namespace We.Processes;

public interface ICommandExecutor : IExecutor
{

}
public class CommandExecutor : BaseExecutor, ICommandExecutor
{
    public CommandExecutor(ExecutorOptions options) : base(options)
    {
    }

    public CommandExecutor(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
