namespace HunieMod.Extensions;

/// <summary>
/// A collection of miscellaneous extensions.
/// </summary>
public static class MiscExtensions
{
    /// <summary>
    /// Returns the next value in sequence for this enum.
    /// </summary>
    public static T Next<T>(this T src) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(src.GetType());
        int index = Array.IndexOf(values, src) + 1;
        return values.Length == index ? values[0] : values[index];
    }

    /// <summary>
    /// Returns the previous value in sequence for this enum.
    /// </summary>
    public static T Previous<T>(this T src) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(src.GetType());
        int index = Array.IndexOf(values, src) - 1;
        return index == -1 ? values[values.Length - 1] : values[index];
    }

    /// <summary>
    /// Returns a random value for this enum.
    /// </summary>
    public static T Random<T>(this T _) where T : Enum
    {
        T[] values = Enum.GetValues(typeof(T)) as T[];
        return values[UnityEngine.Random.Range(0, values.Length)];
    }

    /// <summary>
    /// Shuffles an array in place.
    /// </summary>
    public static void Shuffle<T>(this T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i);
            T tmp = array[i];
            array[i] = array[r];
            array[r] = tmp;
        }
    }

    /// <summary>
    /// Shuffles a list in place.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = new System.Random().Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
