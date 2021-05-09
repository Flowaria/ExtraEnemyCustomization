using EECustom.Customizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Configs
{
    public class ScoutWaveConfig
    {
        public ExpeditionScoutSetting[] Expeditions = new ExpeditionScoutSetting[0];
        public ScoutTargetSetting[] TargetSettings = new ScoutTargetSetting[0];
        public ScoutWaveSetting[] WaveSettings = new ScoutWaveSetting[0];
    }

    public class ExpeditionScoutSetting
    {
        public string[] Targets = new string[0]; //A* //*1 //A1
        public string[] ScoutSettingsToUse = new string[0];
    }

    public class ScoutTargetSetting
    {
        public string Name;

        public TargetSetting Target;
        public string WaveSetting;
    }

    public class ScoutWaveSetting
    {
        public string Name;

        public WaveSetting[] Waves = new WaveSetting[0];
    }

    public struct WaveSetting
    {
        public uint WaveSettingID;
        public uint WavePopulationID;
        public float Delay;
    }
}
