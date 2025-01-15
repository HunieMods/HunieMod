namespace HunieMod.Utils;

/// <summary>
/// A collection of general utilities.
/// </summary>
public static class Utilities
{
    private static readonly ManualLogSource logger = Logger.CreateLogSource(nameof(Utilities));

    /// <summary>
    /// Creates a <see cref="DateTime"/> object from a Unix timestamp.
    /// </summary>
    public static DateTime DateFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }

    /// <summary>
    /// Attempts to parse a feet/inch string and returns the length in meters, or <c>null</c> when parsing failed.
    /// </summary>
    public static float? FeetInchesToMeters(string length)
    {
        Match match = Regex.Match(length, "^(?:\\d+')?(?: ?\\d+\"( ?\\d+\\/\\d+)?)?$");
        if (match.Success && match.Groups.Count >= 2)
        {
            string[] split = match.Groups[0].Value.Split('\'');
            if (int.TryParse(split[0], out int feet) && int.TryParse(split[1].Replace("\"", ""), out int inches))
            {
                inches = (feet * 12) + inches;
                return (float)(inches / 39.370078);
            }
        }
        return null;
    }

    /// <summary>
    /// Attempts to parse a weight string and converts it to metric or imperial, depending on the input.
    /// </summary>
    public static string ConvertWeight(string weight)
    {
        float num = Convert.ToSingle(new Regex("[^0-9 -]").Replace(weight, ""));
        if (num > 0)
        {
            if (weight.ToLower().Contains("lbs") || weight.ToLower().Contains("pound"))
            {
                // Convert to kilograms
                return string.Format("{0} KG", Convert.ToInt16(num * 0.453592f));
            }
            else if (weight.ToLower().Contains("kg") || weight.ToLower().Contains("kilo"))
            {
                // Convert to pounds
                return string.Format("{0} lbs", Convert.ToInt16(num * 2.20462f));
            }
            else
            {
                // Don't know unit of mass, return original string
                return weight;
            }
        }
        else
        {
            return weight;
        }
    }

    /// <summary>
    /// Reads the contents of a file, either from a regular file on disk or an embedded resource in an <see cref="Assembly"/>.
    /// </summary>
    /// <param name="filePath">
    /// The path of the file to read. For embedded resources, the syntax is <c>[FullType].Resources.[pathToFile]</c>.
    /// For example: <c>HunieMod.Plugins.MyPlugin.Resources.Images.MyImage.png</c>
    /// </param>
    /// <param name="isEmbeddedResource">Whether the file is a resource embedded in an <see cref="Assembly"/>.</param>
    /// <param name="assembly">The <see cref="Assembly"/> that embeds the file. When not specified, the Assembly where <see cref="Utilities"/> resides wil be used.</param>
    /// <returns>A <see cref="byte"/> array of the file's contents, or <c>null</c> if the file was not found.</returns>
    public static byte[] GetFileContents(string filePath, bool isEmbeddedResource = false, Assembly assembly = null)
    {
        logger.LogWarning($"Readin {filePath}");
        byte[] data;

        if (isEmbeddedResource)
        {
            assembly ??= typeof(Utilities).Assembly;

            using Stream stream = assembly.GetManifestResourceStream(filePath);
            if (stream == null)
            {
                logger.LogWarning($"{nameof(GetFileContents)}: Unable to find resource \"{filePath}\" in assembly {assembly.FullName}");
                return null;
            }
            data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
        }
        else
        {
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"{nameof(GetFileContents)}: File does not exist: {filePath}");
                return null;
            }
            using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
        }

        return data;
    }
}
