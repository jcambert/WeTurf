using System.Globalization;
using System.Numerics;

namespace We.Utilities;

public enum RoundMethod
{
    Round,
    Ceiling,
    Floor
}

public static class NumberExtensions
{
    internal static Func<double, int, double> GetRoundMethod(this RoundMethod method) =>
        method switch
        {
            RoundMethod.Round => Math.Round,
            RoundMethod.Ceiling
              => (double value, int decimalPlace) =>
              {
                  value *= Math.Pow(10, decimalPlace);
                  value = Math.Ceiling(value);
                  return value / Math.Pow(10, decimalPlace);
              },
            RoundMethod.Floor
              => (double value, int decimalPlace) =>
              {
                  return Math.Floor(value * Math.Pow(10, decimalPlace))
                      / Math.Pow(10, decimalPlace);
              },
            _ => throw new NotSupportedException()
        };

    public static double Round(
        this double value,
        int decimalPlace = 2,
        RoundMethod method = RoundMethod.Round
    ) => method.GetRoundMethod()(value, decimalPlace);

    public static string ToN2String<T>(this T value) where T : INumber<T> =>
        value.ToString("0.00", CultureInfo.CurrentCulture);

    public static string ToMoneytary<T>(this T value) where T : INumber<T> =>
        value?.ToString("C0", CultureInfo.CurrentCulture);
}
