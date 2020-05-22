using BepInEx;

namespace HunieMod
{
    /// <summary>
    /// This attribute denotes that a class is a plugin, and specifies the required metadata.
    /// </summary>
    public class HunieModPlugin : BepInPlugin
    {
        /// <summary>
        /// The author of the plugin.
        /// </summary>
        public string Author { get; protected set; }

        public HunieModPlugin(string GUID, string Name, string Version, string Author = null) : base(GUID, Name, Version)
        {
            this.Author = Author;
        }
    }
}
