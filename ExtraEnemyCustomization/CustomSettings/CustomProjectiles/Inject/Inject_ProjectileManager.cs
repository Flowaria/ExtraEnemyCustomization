using EECustom.Managers;
using HarmonyLib;
using System;
using UnityEngine;

namespace EECustom.CustomSettings.CustomProjectiles.Inject
{
    [HarmonyPatch(typeof(ProjectileManager))]
    class Inject_ProjectileManager
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ProjectileManager.LoadAssets))]
        static void Post_LoadAsset()
        {
            foreach (var proj in ConfigManager.Current.ProjectileCustom.ProjectileDefinitions)
            {
                CustomProjectile.GenerateProjectile(proj);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(ProjectileManager.SpawnProjectileType))]
        static bool Pre_SpawnProjectile(ref GameObject __result, ref ProjectileType type, Vector3 pos, Quaternion rot)
        {
            if (Enum.IsDefined(typeof(ProjectileType), (byte)type))
            {
                return true;
            }

            var projectilePrefab = CustomProjectile.GetProjectile((byte)type);
            if (projectilePrefab == null)
            {
                Logger.Error($"CANT FIND PREFAB WITH ID: {(int)type}");
                type = ProjectileType.TargetingSmall;
                return true;
            }

            var gameObject = GameObject.Instantiate(projectilePrefab, pos, rot, ProjectileManager.Current.m_root.transform);
            gameObject.active = true;
            __result = gameObject;
            return false;
        }
    }
}