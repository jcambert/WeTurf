using System.Reflection;
namespace We.Processes;

public class Anaconda : IAnaconda
{
    public Anaconda(DriveService drive = null)
    {
        if (drive is not null)
            this.Drive = drive.ExecutingDriveLetter;
        else
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var drive0 = new DriveInfo(location);
            this.Drive = drive0.ToString();
        }
        BasePath = $@"{Drive}anaconda3";
    }
    public string EnvironmentName { get; init; } = "base";
    public string BasePath { get; init; }
    protected string Drive { get; }
}
