namespace We.Processes;

public class ExecutorOptions
{
    public string WorkingDirectory { get; set; }
    public bool UseReactiveOutput { get; set; } = false;
    public bool ExecuteInConsole { get; set; } = false;
}
