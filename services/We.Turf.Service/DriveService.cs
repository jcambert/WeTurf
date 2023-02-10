using System.Reflection;

namespace We.Turf.Service;

public class DriveService
{
    public string ExecutingDriveLetter
    {
        get
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var drive = new DriveInfo(location);
            return drive.ToString();
        }

    }
    public override string ToString() => ExecutingDriveLetter;
}
