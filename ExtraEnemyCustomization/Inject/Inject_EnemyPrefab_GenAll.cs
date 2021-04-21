using Enemies;
using ExtraEnemyCustomization.Customizations;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace ExtraEnemyCustomization.Inject
{
    [HarmonyPatch(typeof(EnemyPrefabManager), nameof(EnemyPrefabManager.GenerateAllEnemyPrefabs))]
    static class Inject_EnemyPrefab_GenAll
    {
        static void Prefix()
        {
            Logger.Debug("== List of Material that can be used for Materials Parameters ==");

            var fullmats = Resources.FindObjectsOfTypeAll(Il2CppType.Of<Material>());
            foreach (var obj in fullmats)
            {
                var mat = obj.Cast<Material>();
                var matName = mat?.name ?? string.Empty;
                var shaderName = mat?.shader?.name ?? string.Empty;

                if (string.IsNullOrEmpty(matName))
                    continue;

                if (!ConfigContext.Current.CacheAllMaterials)
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
