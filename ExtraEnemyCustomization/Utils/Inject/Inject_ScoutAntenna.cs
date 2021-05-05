using Enemies;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Utils.Inject
{
    [HarmonyPatch(typeof(ScoutAntenna), nameof(ScoutAntenna.Init))]
    class Inject_ScoutAntenna
    {
        static void Postfix(ScoutAntennaDetection detection, ScoutAntenna __instance)
        {
            ScoutAntennaSpawnEvent.OnAntennaSpawn?.Invoke(detection.m_owner, detection, __instance);
        }
    }
}
