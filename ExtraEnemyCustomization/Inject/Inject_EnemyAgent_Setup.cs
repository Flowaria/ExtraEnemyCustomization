using Enemies;
using HarmonyLib;

namespace ExtraEnemyCustomization.Inject
{
    [HarmonyPatch(typeof(EnemyAgent), nameof(EnemyAgent.Setup))]
    internal static class Inject_EnemyAgent_Setup
    {
        private static void Postfix(EnemyAgent __instance)
        {
            ConfigContext.Current.Customize_Postspawn(__instance);
        }
    }
}