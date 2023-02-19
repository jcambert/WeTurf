namespace We.Utilities;

public static class FileExtensions
{
    private static string AdditionnalDate()
    =>$"_{DateOnly.FromDateTime(DateTime.Now).ToString().Replace('/','_')}";
    
    public static string GenerateCopyName(this string filepath,Func<string> AdditionalFn)
    {
        if(string.IsNullOrEmpty(filepath))
        {
            throw new ArgumentNullException(filepath, nameof(filepath));
        }

        AdditionalFn = AdditionalFn ?? AdditionnalDate;

        string filename = Path.GetFileNameWithoutExtension(filepath);
        string extension = Path.GetExtension(filepath);
        string directory = Path.GetDirectoryName(filepath) ?? string.Empty  ;

        string newFilename = $"{directory}{filename}{AdditionalFn()}{extension}";
        return newFilename;
    }
}