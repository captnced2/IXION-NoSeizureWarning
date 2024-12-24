using BulwarkStudios.Stanford.Demo;
using HarmonyLib;

namespace NoSeizureWarning;

public class Patches
{
    [HarmonyPatch(typeof(GameInitialize), nameof(GameInitialize.Start))]
    public static class BuildingInstancePatcher
    {
        public static void Postfix(GameInitialize __instance)
        {
            foreach (var v in __instance.steps)
                if (v.waitDuration.Equals(4))
                {
                    v.waitDuration = 2;
                }
                else if (v.waitDuration.Equals(30))
                {
                    v.fadeInDuration = 0;
                    v.waitDuration = 0;
                    v.fadeOutDuration = 0;
                }
        }
    }
}