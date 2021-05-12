using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Events.Inject
{
    [HarmonyPatch(typeof(ScoutAntennaDetection), nameof(ScoutAntennaDetection.OnSpawn))]
    class Inject_ScoutAntennaDetection
    {
        [HarmonyWrapSafe]
        static void Prefix(pScoutAntennaDetectionSpawnData spawnData, ScoutAntennaDetection __instance)
        {
            if(spawnData.owner.TryGet(out var owner))
            {
                ScoutAntennaSpawnEvent.OnDetectionSpawn?.Invoke(owner, __instance);
            }
        }
    }
}
