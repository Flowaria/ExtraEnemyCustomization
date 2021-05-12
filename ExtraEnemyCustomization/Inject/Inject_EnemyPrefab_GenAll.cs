using Enemies;
using EECustom.Customizations;
using HarmonyLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using EECustom.Customizations.Models;
using EECustom.Managers;
using System;

namespace EECustom.Inject
{
    [HarmonyPatch(typeof(EnemyPrefabManager), nameof(EnemyPrefabManager.GenerateAllEnemyPrefabs))]
    internal static class Inject_EnemyPrefab_GenAll
    {
        [HarmonyWrapSafe]
        private static void Prefix()
        {
            Logger.Debug("== List of Material that can be used for Materials Parameters ==");

            //TODO: Replace this to AssetShardManager Code
            var fullmats = Resources.FindObjectsOfTypeAll(Il2CppType.Of<Material>());
            foreach (var obj in fullmats)
            {
                var mat = obj.Cast<Material>();
                var matName = mat?.name ?? string.Empty;
                var shaderName = mat?.shader?.name ?? string.Empty;

                if (string.IsNullOrEmpty(matName))
                    continue;

                if (!ConfigManager.Current.ModelCustom.CacheAllMaterials)
                {
                    if (!shaderName.Contains("EnemyFlesh"))
                        continue;
                }

                MaterialCustom.AddToCache(matName, mat);
                Logger.Debug(matName);
            }

            Logger.Debug("== End of List ==");
        }
    }
}