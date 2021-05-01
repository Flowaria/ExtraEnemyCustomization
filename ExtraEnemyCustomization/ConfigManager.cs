using Enemies;
using EECustom.Configs;
using EECustom.Customizations;
using EECustom.Utils;
using MTFO.Managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EECustom
{
    public class ConfigManager
    {
        public static void Initialize()
        {
            Current = new ConfigManager();

            if (MTFO.Managers.ConfigManager.HasCustomContent)
            {
                try
                {
                    var basePath = Path.Combine(MTFO.Managers.ConfigManager.CustomPath, "ExtraEnemyCustomization");

                    Logger.Debug("Loading Model.json...");
                    if (TryLoadConfig(basePath, "Model.json", out ModelCustomConfigData modelConfig))
                        Current.ModelCustom = modelConfig;

                    Logger.Debug("Loading Ability.json...");
                    if (TryLoadConfig(basePath, "Ability.json", out AbilityCustomConfigData abilityConfig))
                        Current.AbilityCustom = abilityConfig;

                    Logger.Debug("Loading Projectile.json...");
                    if (TryLoadConfig(basePath, "Projectile.json", out ProjectileCustomConfigData projConfig))
                        Current.ProjectileCustom = projConfig;

                    Logger.Debug("Loading Tentacle.json...");
                    if (TryLoadConfig(basePath, "Tentacle.json", out TentacleCustomConfigData tentacleConfig))
                        Current.TentacleCustom = tentacleConfig;

                    Logger.Debug("Loading Scout.json...");
                    if (TryLoadConfig(basePath, "Scout.json", out ScoutCustomConfigData scoutConfig))
                        Current.ScoutCustom = scoutConfig;
                }
                catch (Exception e)
                {
                    Logger.Error($"Error Occured While reading ExtraEnemyCustomization.json file: {e}");
                }
            }
            else
            {
                Logger.Warning("No Custom content were found, No Customization will be applied");
            }

            Current.GenerateBuffer();
        }

        public static bool TryLoadConfig<T>(string basePath, string fileName, out T config)
        {
            var path = Path.Combine(basePath, fileName);
            if(File.Exists(path))
            {
                try
                {
                    config = JSON.Deserialize<T>(File.ReadAllText(path));
                    return true;
                }
                catch(Exception e)
                {
                    Logger.Error($"Exception Occured While reading {path} file: {e}");
                    config = default;
                    return false;
                }
            }
            else
            {
                Logger.Warning($"File: {path} is not exist, ignoring this config...");
                config = default;
                return false;
            }
        }
        public static ConfigManager Current { get; private set; }
        
        public ModelCustomConfigData ModelCustom { get; private set; } = new ModelCustomConfigData();
        public AbilityCustomConfigData AbilityCustom { get; private set; } = new AbilityCustomConfigData();
        public ProjectileCustomConfigData ProjectileCustom { get; private set; } = new ProjectileCustomConfigData();
        public TentacleCustomConfigData TentacleCustom { get; private set; } = new TentacleCustomConfigData();
        public ScoutCustomConfigData ScoutCustom { get; private set; } = new ScoutCustomConfigData();

        private readonly List<EnemyCustomBase> _CustomizationBuffer = new List<EnemyCustomBase>();

        private void GenerateBuffer()
        {
            _CustomizationBuffer.Clear();
            _CustomizationBuffer.AddRange(ModelCustom.GetAllSettings());
            _CustomizationBuffer.AddRange(AbilityCustom.GetAllSettings());
            _CustomizationBuffer.AddRange(ProjectileCustom.GetAllSettings());
            _CustomizationBuffer.AddRange(TentacleCustom.GetAllSettings());
            foreach (var custom in _CustomizationBuffer)
            {
                custom.Initialize();
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