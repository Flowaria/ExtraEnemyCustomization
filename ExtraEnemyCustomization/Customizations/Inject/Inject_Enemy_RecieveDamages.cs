using Agents;
using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.Customizations.Inject
{
    public static class EnemyDamageEvents
    {
        public static Action<EnemyAgent, Agent, float> OnDamage;
    }

    [HarmonyPatch(typeof(Dam_EnemyDamageBase))]
    class Inject_Enemy_RecieveDamages
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Dam_EnemyDamageBase.ProcessReceivedDamage))]
        static void Post_ProcDamage(float damage, Agent damageSource, Dam_EnemyDamageBase __instance)
        {
            EnemyDamageEvents.OnDamage?.Invoke(__instance.Owner, damageSource, damage);
        }
    }
}
