using BepInEx.Logging;

namespace YetAnotherPriceTooltip;

public static class Logging {
    internal static void Log(object payload, LogLevel level = LogLevel.Info) {
        // This doesn't error, Rider just hasn't caught up
        YetAnotherPriceTooltip.Logger.Log(level, payload);
    }
}