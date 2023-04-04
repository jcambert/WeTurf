namespace We.Utilities;

public static class FileExtensions
{
    private static string AdditionnalDate()
    => $"_{DateOnly.FromDateTime(DateTime.Now).ToString().Replace('/', '_')}";

    public static string GenerateCopyName(this string filepath, Func<string> AdditionalFn)
    {
        if (string.IsNullOrEmpty(filepath))
        {
            throw new ArgumentNullException(filepath, nameof(filepath));
        }

        AdditionalFn = AdditionalFn ?? AdditionnalDate;

        string filename = Path.GetFileNameWithoutExtension(filepath);
        string extension = Path.GetExtension(filepath);
        string directory = Path.GetDirectoryName(filepath) ?? string.Empty;

        string newFilename = $"{directory}/{filename}{AdditionalFn()}{extension}";
        return newFilename;
    }
    public static string EnsureStartWith(this string s,string sw)
    {
        if (string.IsNullOrEmpty(s))
        {
            return sw;
        }
        if (!s.StartsWith(sw))
            return $"{sw}{s}";
        return s;
    }
    public static bool Filename(this string filepath, out Filename filename)
    {
        try
        {
            string dossier = Path.GetDirectoryName(filepath);
            string nomFichier = Path.GetFileNameWithoutExtension(filepath);
            string extension = Path.GetExtension(filepath);
            filename=new Filename(dossier, nomFichier, extension);
            return true;
        }
        catch 
        {
            filename = default;
            return false;
        }

    }
}

public sealed record Filename(string Folder, string Name, string Extension)
{
   
    public static implicit operator string(Filename filename)
    => Path.Combine(filename.Folder, filename.Name + filename.Extension);
    public static implicit operator Filename((string, string, string) filename)
    => new Filename(filename.Item1,filename.Item2,filename.Item3.EnsureStartWith(""));
    public static implicit operator Filename(string filename)
    {
       if(filename.Filename(out Filename result))
            return result;
        return default;
    }
        

}