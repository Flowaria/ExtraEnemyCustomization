using HarmonyLib;

namespace ExtraEnemyCustomization.CustomProjectiles.Inject
{
    [HarmonyPatch(typeof(ProjectileManager), nameof(ProjectileManager.LoadAssets))]
    internal class Inject_ProjectileManager_LoadAssets
    {
        private static void Postfix()
        {
            foreach (var proj in ConfigContext.Current.ProjectileDefinitions)
            {
                CustomProjectile.GenerateProjectile(proj);
            }
        }
    }
}