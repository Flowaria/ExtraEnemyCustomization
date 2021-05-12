using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.CustomSettings.Inject
{
    [HarmonyPatch(typeof(RundownManager), nameof(RundownManager.SetActiveExpedition))]
    class Inject_RundownManager
    {
        static void Postfix()
        {
            var rawKey = RundownManager.ActiveExpeditionUniqueKey;
            if (string.IsNullOrEmpty(rawKey))
            {
                Logger.Log("KEY WAS EMPTY");
                return;
            }
            var split = rawKey.Split("-");
            if (split.Length != 3)
            {
                Logger.Warning($"Key split length was not 3?: Length: {split.Length} Raw: {rawKey}");
                return;
            }

            if (!Enum.TryParse(split[1], out eRundownTier tier))
            {
                Logger.Warning($"Tier is not valid?: {split[1]}");
                return;
            }

            if (!int.TryParse(split[2], out var index))
            {
                Logger.Warning($"Index was not a number?: {split[2]}");
                return;
            }

            CustomScoutWaveManager.ExpeditionUpdate(tier, index, RundownManager.ActiveExpedition);
        }
    }
}
