using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Events.Inject
{
    [HarmonyPatch(typeof(EnemyAgent), nameof(EnemyAgent.SyncPlaceNavMarkerTag))]
    class Inject_Enemy_Marked
    {
        [HarmonyWrapSafe]
        static void Postfix(EnemyAgent __instance)
        {
            EnemyMarkerEvents.OnMarked?.Invoke(__instance, __instance.m_tagMarker);
        }
    }
}
