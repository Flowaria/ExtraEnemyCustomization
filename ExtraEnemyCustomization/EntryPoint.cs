using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using EECustom.Customizations.Abilities.Managers;
using EECustom.Managers;
using HarmonyLib;
using UnhollowerRuntimeLib;

namespace EECustom
{
    //TODO: Refactor the CustomBase to support Phase Setting
    //TODO: Scout WaveSetting Custom

    [BepInPlugin("GTFO.EECustomization", "EECustomization", "0.4.0")]
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

            var useDevMsg = Config.Bind(new ConfigDefinition("Logging", "UseDevMessage"), false, new ConfigDescription("Using Dev Message for Debugging your config?"));
            var useVerbose = Config.Bind(new ConfigDefinition("Logging", "Verbose"), false, new ConfigDescription("Using Much more detailed Message for Debugging?"));

            Logger.UsingDevMessage = useDevMsg.Value;
            Logger.UsingVerbose = useVerbose.Value;

            var harmony = new Harmony("EECustomization.Harmony");
            harmony.PatchAll();

            ConfigManager.Initialize();

            
        }
    }
}