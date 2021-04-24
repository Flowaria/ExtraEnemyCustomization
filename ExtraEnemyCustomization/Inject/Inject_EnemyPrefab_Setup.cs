using Enemies;
using HarmonyLib;
using UnityEngine;

namespace ExtraEnemyCustomization.Inject
{
    [HarmonyPatch(typeof(EnemyPrefabManager), nameof(EnemyPrefabManager.BuildEnemyPrefab))]
    internal static class Inject_EnemyPrefab_Setup
    {
        private static void Postfix(GameObject __result)
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