using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;

namespace YetAnotherPriceTooltip;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
internal class YetAnotherPriceTooltip : BaseUnityPlugin {
    internal static new ManualLogSource Logger { get; private set; }
    public static new ConfigFile Config { get; private set; }
    private static Settings Settings;

    private void Awake() {
        Logger = base.Logger;
        Config = base.Config;
        Settings = new Settings(Config);
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
        Logging.Log($"{MyPluginInfo.PLUGIN_NAME} version {MyPluginInfo.PLUGIN_VERSION} successfully loaded", LogLevel.Message);
    }
}