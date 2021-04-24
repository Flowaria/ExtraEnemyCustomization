using Enemies;
using ExtraEnemyCustomization.Customizations;
using ExtraEnemyCustomization.CustomProjectiles;
using MTFO.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExtraEnemyCustomization
{
    public class ConfigContext
    {
        public static void Initialize()
        {
            if (ConfigManager.HasCustomContent)
            {
                try
                {
                    var path = Path.Combine(ConfigManager.CustomPath, "ExtraEnemyCustomization.json");
                    Current = JsonConvert.DeserializeObject<ConfigContext>(File.ReadAllText(path));
                }
                catch (Exception e)
                {
                    Logger.Error($"Error Occured While reading ExtraEnemyCustomization.json file: {e}");
                    Current = new ConfigContext();
                }
            }
            else
            {
                Logger.Warning("No Custom content were found, No Customization will be applied");
                Current = new ConfigContext();
            }

            Current.GenerateBuffer();
        }

        [JsonIgnore]
        public static ConfigContext Current { private set; get; }

        public bool CacheAllMaterials = false;
        public ShadowCustom[] ShadowModelCustom = new ShadowCustom[0];
        public StrikerTentacleCustom[] StrikerTentacleCustom = new StrikerTentacleCustom[0];
        public ShooterFireCustom[] ShooterFireCustom = new ShooterFireCustom[0];
        public BirthingCustom[] BirthingCustom = new BirthingCustom[0];
        public FogSphereCustom[] FogSphereCustom = new FogSphereCustom[0];
        public MaterialCustom[] MaterialCustom = new MaterialCustom[0];
        public LimbCustom[] LimbCustom = new LimbCustom[0];

        public CustomProjectile[] ProjectileDefinitions = new CustomProjectile[0];

        [JsonIgnore]
        private List<EnemyCustomBase> _CustomizationBuffer = new List<EnemyCustomBase>();

        private void GenerateBuffer()
        {
            _CustomizationBuffer.Clear();
            _CustomizationBuffer.AddRange(ShadowModelCustom);
            _CustomizationBuffer.AddRange(StrikerTentacleCustom);
            _CustomizationBuffer.AddRange(ShooterFireCustom);
            _CustomizationBuffer.AddRange(BirthingCustom);
            _CustomizationBuffer.AddRange(FogSphereCustom);
            _CustomizationBuffer.AddRange(MaterialCustom);
            _CustomizationBuffer.AddRange(LimbCustom);

            Logger.Debug("ShadowModel Custom Settings:");
            foreach (var shadowCustom in ShadowModelCustom)
            {
                Logger.Debug($"- {shadowCustom.Target.ToDebugString()}");
                Logger.Debug($"- CustomInfo, reqTagForDetect: {shadowCustom.RequireTagForDetection}, includeEgg: {shadowCustom.IncludeEggSack}");
            }

            Logger.Debug("StrikerTentacles Custom Settings:");
            foreach (var tenCustom in StrikerTentacleCustom)
            {
                Logger.Debug($"- {tenCustom.Target.ToDebugString()}");
                Logger.Debug($"- CustomInfo, TypePattern: [{string.Join(", ", tenCustom.TentacleTypes)}]");
            }

            Logger.Debug("Material Custom Settings:");
            foreach (var matCustom in MaterialCustom)
            {
                Logger.Debug($"- {matCustom.Target.ToDebugString()}");
                foreach (var swapSet in matCustom.MaterialSets)
                {
                    Logger.Debug($"- SwapSetInfo, FromMat: {swapSet.From}, ToMat: {swapSet.To}");
                }
            }
        }

        public void Customize_Prespawn(EnemyAgent agent)
        {
            foreach (var custom in _CustomizationBuffer)
            {
                if (!custom.Enabled)
                    continue;

                if (custom.Target.IsMatch(agent) && custom.HasPrespawnBody)
                {
                    custom.LogDev($"Applying Prespawn effect to {agent.name}");
                    custom.Prespawn(agent);
                    custom.LogDev($"Finished!");
                }
            }
        }

        public void Customize_Postspawn(EnemyAgent agent)
        {
            foreach (var custom in _CustomizationBuffer)
            {
                if (!custom.Enabled)
                    continue;

                if (custom.Target.IsMatch(agent) && custom.HasPostspawnBody)
                {
                    custom.LogDev($"Applying Postspawn effect to {agent.name}");
                    custom.Postspawn(agent);
                    custom.LogDev($"Finished!");
                }
            }
        }
    }
}