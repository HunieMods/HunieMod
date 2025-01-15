namespace HunieMod.Extensions;

/// <summary>
/// A collection of utilities for <see cref="Enum"/> types.
/// </summary>
public static class EnumExtensions
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
}
