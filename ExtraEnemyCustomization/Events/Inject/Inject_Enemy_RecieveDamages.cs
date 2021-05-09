using Agents;
using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EECustom.Events.Inject
{
    [HarmonyPatch(typeof(Dam_EnemyDamageBase))]
    class Inject_Enemy_RecieveDamages
    {
        //NOTE: Hooking Dam_EnemyDamageBase.ProcessReceivedDamage will cause various problem, such as unwanted hitreact.

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Dam_EnemyDamageBase.ReceiveBulletDamage))]
        static void Post_BulletDamage(pBulletDamageData data, Dam_EnemyDamageBase __instance)
        {
            data.source.TryGet(out var agent);
            EnemyDamageEvents.OnDamage?.Invoke(__instance.Owner, agent);
        }


        [HarmonyPostfix]
        [HarmonyPatch(nameof(Dam_EnemyDamageBase.ReceiveMeleeDamage))]
        static void Post_MeleeDamage(pFullDamageData data, Dam_EnemyDamageBase __instance)
        {
            data.source.TryGet(out var agent);
            EnemyDamageEvents.OnDamage?.Invoke(__instance.Owner, agent);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Dam_EnemyDamageBase.ReceiveExplosionDamage))]
        static void Post_ExplosionDamage(pFullDamageData data, Dam_EnemyDamageBase __instance)
        {
            data.source.TryGet(out var agent);
            EnemyDamageEvents.OnDamage?.Invoke(__instance.Owner, agent);
        }
    }
}
