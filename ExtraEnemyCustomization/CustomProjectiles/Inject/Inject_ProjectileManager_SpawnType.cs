using HarmonyLib;
using System;
using UnityEngine;

namespace ExtraEnemyCustomization.CustomProjectiles.Inject
{
    [HarmonyPatch(typeof(ProjectileManager), nameof(ProjectileManager.SpawnProjectileType))]
    internal class Inject_ProjectileManager_SpawnType
    {
        private static bool Prefix(ref GameObject __result, ref ProjectileType type, Vector3 pos, Quaternion rot)
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