using System.IO;
using System.Threading.Tasks;

namespace We.Turf.Blazor.Components;

public class FileWithContent
{
    public PmuFileType Type { get; init; }
    public string Path { get; init; }
    public bool IsVisible { get; set; } = false;
    public bool IsLoading { get; set; } = false;
    public string Content { get; set; } = string.Empty;
    public async Task RefreshFileContent()
    {
        if(!IsVisible) return;
        IsLoading = true;
        Content = await File.ReadAllTextAsync(Path);
        IsLoading = false;
    }
}
public enum PmuFileType
{
    ToPredict,
    Predicted,
    Resultat,
    Course
}
