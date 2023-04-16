namespace We.Utilities;

public static class ObjectExtensions
{
    public static IEnumerable<T> TakeOnly<T>(this IEnumerable<T> values, int[] indexes)
    {
        if(values.Count()<indexes.Length)
            throw new ArgumentOutOfRangeException($"{nameof(values)} has less element than {nameof(indexes)} ");
        var temp=values.ToArray();
        for (int i = 0; i < indexes.Length; i++)
        {
            yield return temp[indexes[i]];
        }
    }
}
