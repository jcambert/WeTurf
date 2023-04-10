namespace We.Processes;

public class PythonExecutorOptions : ExecutorOptions
{
    public bool UseAnaconda { get; set; } = false;
    
    public string AnacondBasePath { get; set; } = string.Empty;
    public string EnvironmentName { get; set; } = "base";
    public string PythonPath { get; set; } = string.Empty;
}
