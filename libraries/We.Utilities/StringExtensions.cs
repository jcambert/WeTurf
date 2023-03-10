namespace We.Utilities;

public static class StringExtensions
{
    public static string[] Split(this string value, int part)
    => value.SplitEnumerable(part).ToArray();

    public static IEnumerable<string> SplitEnumerable(this string value, int part)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (part <= 0)
            throw new ArgumentOutOfRangeException($"{nameof(part)} must be >0");

        for (int i = 0; i < value.Length; i += part)
        {
            yield return value.Substring(i, part).Trim();
        }
    }
}