using BepInEx;

namespace HunieMod
{
    /// <inheritdoc/>
    public class HunieModPlugin : BepInPlugin
    {
        /// <summary>
        /// The author of the plugin.
        /// </summary>
        public string Author { get; protected set; }

        /// <param name="GUID">The unique identifier of the plugin. Should not change between plugin versions.</param>
        /// <param name="Name">The user friendly name of the plugin. Is able to be changed between versions.</param>
        /// <param name="Version">The specfic version of the plugin.</param>
        /// <param name="Author">The author of the plugin.</param>
        public HunieModPlugin(string GUID, string Name, string Version, string Author = null) : base(GUID, Name, Version)
        {
            this.Author = Author;
        }
    }
}
