using System.Security.Cryptography;

namespace We.Utilities;

public enum Padding
{
    Left,
    Right,
    Both
}

public static class StringExtensions
{
    public static string[] Split(this string value, int part) =>
        value.SplitEnumerable(part).ToArray();

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

    public static string EnsureEndsWith(this string value, string suffix)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrEmpty(value))
            return value;
        if (!value.EndsWith(suffix))
            return $"{value}{suffix}";
        return value;
    }

    public static string Pad(
        this string value,
        int part,
        char paddingChar = ' ',
        Padding padding = Padding.Both,
        Padding final = Padding.Right
    )
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value.Length >= part)
            return value;

        // 10 Both 5 cars final.right=>*10*
        //  4 Both 5 cars final.right=>**4**

        int bothCount = ((int)(part - value.Length) / 2);

        (int padLeft, int padRight) = padding switch
        {
            Padding.Right => (0, part),
            Padding.Left => (part, 0),
            _ => (value.Length + bothCount, value.Length + bothCount * 2)
        };
        Func<string, Padding, string> padFunc = (value, padding) =>
            padding switch
            {
                Padding.Left => value.PadLeft(padLeft, paddingChar),
                Padding.Right => value.PadRight(padRight, paddingChar),
                _ => value.PadLeft(padLeft, paddingChar).PadRight(padRight, paddingChar),
            };
        string res = padFunc(value, padding);

        if (res.Count() < part)
        {
            (padLeft, padRight) = final switch
            {
                Padding.Left => (part, 0),
                Padding.Right => (0, part),
                _
                  => throw new NotSupportedException(
                      "You must specify Left or Right for the final padding"
                  )
            };
            res = padFunc(res, final);
        }
        return res;
    }
}
