using System.Text.RegularExpressions;

namespace We.Utilities;

public static class RegexExtensions
{
    public static int[] ToIntArray(this MatchCollection matches, int defaultValue = 0)
    {
        int[] res = new int[matches.Count];
        int idx = 0;
        foreach (Match match in matches)
        {
            if (match.Success && int.TryParse(match.Value, out int v))
                res[idx++] = v;
            else
                res[idx++] = defaultValue;
        }
        return res;
    }
}
