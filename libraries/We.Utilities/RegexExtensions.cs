using System.Text.RegularExpressions;

namespace We.Utilities;

public static class RegexExtensions
{
    public static int[] ToIntArray(this MatchCollection matches,int defaultValue=0)
    {
        int[] res=new int[matches.Count];
        foreach (Match match in matches)
        {
            if (match.Success && int.TryParse(match.Value,out int v))
                res[match.Index] = v;
            else
                res[match.Index] = defaultValue;
        }
        return res;

    }
}
