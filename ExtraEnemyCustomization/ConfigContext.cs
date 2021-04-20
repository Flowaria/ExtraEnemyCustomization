using Enemies;
using ExtraEnemyCustomization.Customizations;
using ExtraEnemyCustomization.CustomProjectiles;
using MTFO.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExtraEnemyCustomization
{
    public class ConfigContext
    {
        public static void Initialize()
        {
            if(ConfigManager.HasCustomContent)
            {
                try
                {
                    var path = Path.Combine(ConfigManager.CustomPath, "ExtraEnemyCustomization.json");
                    Current = JsonConvert.DeserializeObject<ConfigContext>(File.ReadAllText(path));
                }
                catch(Exception e)
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
        public ShadowCustomization[] ShadowModels = new ShadowCustomization[0];
        public StrikerTentacleCustomization[] StrikerTentacles = new StrikerTentacleCustomization[0];
        public ShooterFireCustomization[] ShooterFires = new ShooterFireCustomization[0];
        public FogSphereCustomization[] FogSpheres = new FogSphereCustomization[0];
        public MaterialCustomization[] Materials = new MaterialCustomization[0];

        public CustomProjectile[] ProjectileDefinitions = new CustomProjectile[0];

        [JsonIgnore]
        private List<EnemyCustomizationBase> _CustomizationBuffer = new List<EnemyCustomizationBase>();

        private void GenerateBuffer()
        {
            _CustomizationBuffer.Clear();
            _CustomizationBuffer.AddRange(ShadowModels);
            _CustomizationBuffer.AddRange(StrikerTentacles);
            _CustomizationBuffer.AddRange(ShooterFires);
            _CustomizationBuffer.AddRange(FogSpheres);
            _CustomizationBuffer.AddRange(Materials);

            Logger.DevMessage("ShadowModel Custom Settings:");
            foreach(var shadowCustom in ShadowModels)
            {
                Logger.DevMessage($"- {shadowCustom.Target.ToDebugString()}");
                Logger.DevMessage($"- CustomInfo, reqTagForDetect: {shadowCustom.RequireTagForDetection}, includeEgg: {shadowCustom.IncludeEggSack}");
            }

            Logger.DevMessage("StrikerTentacles Custom Settings:");
            foreach(var tenCustom in StrikerTentacles)
            {
                Logger.DevMessage($"- {tenCustom.Target.ToDebugString()}");
                Logger.DevMessage($"- CustomInfo, TypePattern: [{string.Join(", ", tenCustom.TentacleTypes)}]");
            }

            Logger.DevMessage("Material Custom Settings:");
            foreach(var matCustom in Materials)
            {
                Logger.DevMessage($"- {matCustom.Target.ToDebugString()}");
                foreach (var swapSet in matCustom.MaterialSets)
                {
                    Logger.DevMessage($"- SwapSetInfo, FromMat: {swapSet.From}, ToMat: {swapSet.To}");
                }
            }
        }

        public void Customize_Prespawn(EnemyAgent agent)
        {
            foreach(var custom in _CustomizationBuffer)
            {
                if (!custom.Enabled)
                    continue;

                if(custom.Target.IsMatch(agent))
                {
                    custom.PreSpawn(agent);
                }
            }
        }

        public void Customize_Postspawn(EnemyAgent agent)
        {
            foreach (var custom in _CustomizationBuffer)
            {
                if (!custom.Enabled)
                    continue;

                if (custom.Target.IsMatch(agent))
                {
                    custom.PostSpawn(agent);
                }
            }
        }
    }
}
