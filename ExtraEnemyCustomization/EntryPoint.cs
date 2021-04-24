using BepInEx;
using BepInEx.IL2CPP;
using ExtraEnemyCustomization.Customizations;
using HarmonyLib;
using UnhollowerRuntimeLib;

namespace ExtraEnemyCustomization
{
    [BepInPlugin("GTFO.EECustomization", "ExtraEnemyCustomization", "1.0.0.0")]
    [BepInProcess("GTFO.exe")]
    [BepInDependency(MTFOGUID, BepInDependency.DependencyFlags.HardDependency)]
    public class EntryPoint : BasePlugin
    {
        public const string MTFOGUID = "com.dak.MTFO";

        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<ShooterDistanceConfigManager>();

            Logger.LogInstance = Log;

            var harmony = new Harmony("EECustomization.Harmony");
            harmony.PatchAll();

            ConfigContext.Initialize();
        }
    }
}