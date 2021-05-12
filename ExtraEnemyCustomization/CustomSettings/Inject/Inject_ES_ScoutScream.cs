using Enemies;
using HarmonyLib;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.CustomSettings.Inject
{
    
    [HarmonyPatch(typeof(ES_ScoutScream), "CommonUpdate")]
    class Inject_ES_ScoutScream
    {
        //TODO: Check if it's work
        [HarmonyWrapSafe]
        static void Postfix(ES_ScoutScream __instance)
        {
            if (__instance.m_state == ES_ScoutScream.ScoutScreamState.Response)
            {
                if (SNet.IsMaster)
                {
                    var id = __instance.m_enemyAgent.EnemyDataID;
                    CustomScoutWaveManager.TriggerScoutWave(__instance.m_enemyAgent);
                }
            }
        }
    }
}
