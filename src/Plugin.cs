using System.Reflection;
using System.Runtime.CompilerServices;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace DisplayPiety;

public static class ModInfo
{
    public const string Guid = "Elin.DisplayPiety";
    public const string Name = "Display Piety";
    public const string Version = "1.0.0";
}

[BepInPlugin(ModInfo.Guid, ModInfo.Name, ModInfo.Version)]
internal class Plugin : BaseUnityPlugin
{
    internal static Plugin? Instance;
    internal ConfigEntry<bool>? ShowFaithValue;

    private void Awake()
    {
        Instance = this;
        ShowFaithValue =
            Config.Bind("General", "ShowFaithValue", true, "Show faith skill value.");
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), ModInfo.Guid);
    }

    internal static void LogDebug(object message, [CallerMemberName] string caller = "")
    {
        Instance?.Logger.LogDebug($"[{caller}] {message}");
    }

    internal static void LogInfo(object message)
    {
        Instance?.Logger.LogInfo(message);
    }

    internal static void LogError(object message)
    {
        Instance?.Logger.LogError(message);
    }
}