using System.Runtime.CompilerServices;

namespace We.Turf;

public static class TurfDomainConstants
{
    public const string ALL_CLASSIFIER = "Tous";
    public static readonly DateOnly MIN_DATE = new DateOnly(2023, 1, 1);

    public static bool IsAllClassifier(this string value) =>
        string.IsNullOrEmpty(value) ? false : (value == ALL_CLASSIFIER);
}
