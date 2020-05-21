using BepInEx;

namespace HunieMod
{
    /// <summary>
    /// The base plugin type that adds HunieMod specific functionality over the default BepInEx plugin loader
    /// </summary>
    [HunieModPlugin("org.lounger.huniemod", "HunieMod Base", "1.0.0.0", "Lounger")]
    public class BaseHunieModPlugin : BaseUnityPlugin
    {
    }
}
