using Agents;
using HarmonyLib;
using Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Events.Inject
{
    [HarmonyWrapSafe]
    [HarmonyPatch(typeof(Dam_PlayerDamageLocal))]
    class Inject_PlayerAgent_ReceiveDamages
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Dam_PlayerDamageLocal.ReceiveMeleeDamage))]
        static void Post_Melee(pFullDamageData data, Dam_PlayerDamageBase __instance)
        {
            if (data.source.TryGet(out var inflictor))
            {
                var damage = data.damage.Get(__instance.HealthMax);
                PlayerDamageEvents.OnDamage?.Invoke(__instance.Owner, inflictor, damage);
                PlayerDamageEvents.OnMeleeDamage?.Invoke(__instance.Owner, inflictor, damage);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Dam_PlayerDamageLocal.ReceiveTentacleAttackDamage))]
        static void Post_Tentacle(pMediumDamageData data, Dam_PlayerDamageLocal __instance)
        {
            if (data.source.TryGet(out var inflictor))
            {
                var damage = data.damage.Get(__instance.HealthMax);
                PlayerDamageEvents.OnDamage?.Invoke(__instance.Owner, inflictor, damage);
                PlayerDamageEvents.OnTentacleDamage?.Invoke(__instance.Owner, inflictor, damage);
            }
        }
    }
}
