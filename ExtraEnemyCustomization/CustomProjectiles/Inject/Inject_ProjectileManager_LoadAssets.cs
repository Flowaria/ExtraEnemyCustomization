using ExtraEnemyCustomization.Customizations;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.CustomProjectiles.Inject
{
    [HarmonyPatch(typeof(ProjectileManager), nameof(ProjectileManager.LoadAssets))]
    class Inject_ProjectileManager_LoadAssets
    {
        static void Postfix()
        {
            foreach(var proj in ConfigContext.Current.ProjectileDefinitions)
            {
                CustomProjectile.GenerateProjectile(proj);
            }
        }
    }
}
