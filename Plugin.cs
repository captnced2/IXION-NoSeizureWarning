using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace NoSeizureWarning;

[BepInPlugin(Guid, Name, Version)]
[BepInProcess("IXION.exe")]
public class Plugin : BasePlugin
{
    private const string Guid = "captnced.NoSeizureWarning";
    private const string Name = "No Seizure Warning";
    private const string Version = "1.0.0";
    internal new static ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
        var harmony = new Harmony(Guid);
        harmony.PatchAll();
        foreach (var patch in harmony.GetPatchedMethods())
            Log.LogInfo("Patched " + patch.DeclaringType + ":" + patch.Name);
        Log.LogInfo("Loaded \"" + Name + "\" version " + Version + "!");
    }
}