using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ExtraEnemyCustomization.Inject
{
    [HarmonyPatch(typeof(EnemyPrefabManager), nameof(EnemyPrefabManager.BuildEnemyPrefab))]
    static class Inject_EnemyPrefab_Setup
    {
        static void Postfix(GameObject __result)
        {
            var agent = __result.GetComponent<EnemyAgent>();
            if (agent is null)
            {
                Logger.Error($"Agent is null! : {__result.name}");
                return;
            }
            if (agent.m_isSetup)
            {
                return;
            }
            ConfigContext.Current.Customize_Prespawn(__result.GetComponent<EnemyAgent>());
        }
    }
}
