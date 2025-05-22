using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using IMHelper;

namespace NoSeizureWarning;

[BepInPlugin(Guid, Name, Version)]
[BepInProcess("IXION.exe")]
[BepInDependency("captnced.IMHelper", BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BasePlugin
{
    private const string Guid = "captnced.NoSeizureWarning";
    private const string Name = "No Seizure Warning";
    private const string Version = "1.1.0";
    internal new static ManualLogSource Log;
    private static bool enabled = true;
    private static Harmony harmony;

    public override void Load()
    {
        Log = base.Log;
        harmony = new Harmony(Guid);
        if (IL2CPPChainloader.Instance.Plugins.ContainsKey("captnced.IMHelper")) enabled = ModsMenu.isSelfEnabled();
        if (!enabled)
            Log.LogInfo("Disabled by IMHelper!");
        else
            init();
    }
    
    private static void init()
    {
        harmony.PatchAll();
        foreach (var patch in harmony.GetPatchedMethods())
            Log.LogInfo("Patched " + patch.DeclaringType + ":" + patch.Name);
        Log.LogInfo("Loaded \"" + Name + "\" version " + Version + "!");
    }

    private static void disable()
    {
        harmony.UnpatchSelf();
        Log.LogInfo("Unloaded \"" + Name + "\" version " + Version + "!");
    }
    
    public static void enable(bool value)
    {
        enabled = value;
        if (enabled) init();
        else disable();
    }
}