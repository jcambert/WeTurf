namespace We.Utilities;

public static class IEnumerableExtensions
{
    public static void Deconstruct<T>(this IEnumerable<T> seq, out T first, out IEnumerable<T> rest)
    {
        first = seq.FirstOrDefault();
        rest = seq.Skip(1);
    }

    public static void Deconstruct<T>(
        this IEnumerable<T> seq,
        out T first,
        out T second,
        out IEnumerable<T> rest
    ) => (first, (second, rest)) = seq;

    public static void Deconstruct<T>(
        this IEnumerable<T> seq,
        out T first,
        out T second,
        out T third,
        out IEnumerable<T> rest
    ) => (first, second, (third, rest)) = seq;

    public static void Deconstruct<T>(
        this IEnumerable<T> seq,
        out T first,
        out T second,
        out T third,
        out T fourth,
        out IEnumerable<T> rest
    ) => (first, second, third, (fourth, rest)) = seq;

    public static void Deconstruct<T>(
        this IEnumerable<T> seq,
        out T first,
        out T second,
        out T third,
        out T fourth,
        out T fifth,
        out IEnumerable<T> rest
    ) => (first, second, third, fourth, (fifth, rest)) = seq;

    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) =>
        self.Select((item, index) => (item, index));
}
