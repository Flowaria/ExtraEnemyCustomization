using EECustom.Managers;
using Enemies;
using HarmonyLib;
using System;

namespace EECustom.Inject
{
    [HarmonyPatch(typeof(EnemyAgent), nameof(EnemyAgent.Setup))]
    internal static class Inject_EnemyAgent_Setup
    {
        [HarmonyWrapSafe]
        private static void Postfix(EnemyAgent __instance)
        {
            if (__instance.name.EndsWith(")")) //No Replicator Number = Fake call
            {
                return;
            }
            ConfigManager.Current.Customize_Postspawn(__instance);
        }
    }
}