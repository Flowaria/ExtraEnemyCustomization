using BepInEx;
using BepInEx.IL2CPP;
using ExtraEnemyCustomization.Customizations;
using ExtraEnemyCustomization.Customizations.Abilities;
using HarmonyLib;
using UnhollowerRuntimeLib;

namespace ExtraEnemyCustomization
{
    //TODO: Refactor the CustomBase to support Phase Setting

    [BepInPlugin("GTFO.EECustomization", "EECustomization", "0.3.1")]
    [BepInProcess("GTFO.exe")]
    [BepInDependency(MTFOGUID, BepInDependency.DependencyFlags.HardDependency)]
    public class EntryPoint : BasePlugin
    {
        public const string MTFOGUID = "com.dak.MTFO";

        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<ShooterDistSettingAbility>();
            ClassInjector.RegisterTypeInIl2Cpp<HealthRegenAbility>();

            Logger.LogInstance = Log;

            var harmony = new Harmony("EECustomization.Harmony");
            harmony.PatchAll();

            ConfigManager.Initialize();
        }
    }
}