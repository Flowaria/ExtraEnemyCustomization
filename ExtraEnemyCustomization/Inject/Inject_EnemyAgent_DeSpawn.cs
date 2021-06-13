using EECustom.Managers;
using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Inject
{
    [HarmonyPatch(typeof(EnemyAgent), nameof(EnemyAgent.OnDeSpawn))]
    internal static class Inject_EnemyAgent_DeSpawn
    {
        [HarmonyWrapSafe]
        static void Prefix(EnemyAgent __instance)
        {
            ConfigManager.Current.FireDespawnedEvent(__instance);
        }
    }
}
