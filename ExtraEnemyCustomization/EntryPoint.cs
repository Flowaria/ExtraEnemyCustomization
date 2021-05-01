using BepInEx;
using BepInEx.IL2CPP;
using EECustom.Customizations.Abilities.Managers;
using HarmonyLib;
using UnhollowerRuntimeLib;

namespace EECustom
{
    //TODO: Refactor the CustomBase to support Phase Setting
    //TODO: AlertType Customization: ScoutScream, Propagation, PropagationDistance (also with OnlySameArea / AllAreaInZone)
    //TODO: Scout WaveSetting Custom

    [BepInPlugin("GTFO.EECustomization", "EECustomization", "0.3.5")]
    [BepInProcess("GTFO.exe")]
    [BepInDependency(MTFOGUID, BepInDependency.DependencyFlags.HardDependency)]
    public class EntryPoint : BasePlugin
    {
        public const string MTFOGUID = "com.dak.MTFO";

        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<ShooterDistSettingManager>();
            ClassInjector.RegisterTypeInIl2Cpp<HealthRegenManager>();

            Logger.LogInstance = Log;

            var harmony = new Harmony("EECustomization.Harmony");
            harmony.PatchAll();

            ConfigManager.Initialize();
        }
    }
}