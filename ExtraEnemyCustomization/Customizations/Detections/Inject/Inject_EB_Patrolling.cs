using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations.Detections.Inject
{
    //[HarmonyPatch(typeof(EB_Patrolling))]
    class Inject_EB_Patrolling
    {
        [HarmonyPostfix]
        [HarmonyWrapSafe]
        [HarmonyPatch(nameof(EB_Patrolling.UpdateDetection))]
        static void Post_UpdateDetection(EB_Patrolling __instance)
        {
            if (__instance.m_waitingForAbilityDone)
            {

            }
        }
    }
}
