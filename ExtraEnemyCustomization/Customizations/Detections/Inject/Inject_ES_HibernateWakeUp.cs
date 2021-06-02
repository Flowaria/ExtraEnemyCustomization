using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations.Detections.Inject
{
    //[HarmonyPatch(typeof(ES_HibernateWakeUp), nameof(ES_HibernateWakeUp.ActivateState))]
    class Inject_ES_HibernateWakeUp
    {
        static void Postfix(ES_HibernateWakeUp __instance)
        {
            Logger.Log("Detected Wakeup! (From HibernateWakeup)");
            __instance.m_ai.m_locomotion.ScoutScream.ActivateState(__instance.m_ai.Target);
        }
    }
}
