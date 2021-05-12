using EECustom.Customizations;
using GameData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.CustomSettings.DTO
{
    public class ScoutWaveSettings
    {
        private uint _DefaultWaveSettingID;
        private uint _DefaultWavePopulationID;
    }

    public class ExpeditionScoutSetting
    {
        public string[] Targets = new string[0]; //A* //*1 //A1
        public ScoutWaveSet[] ScoutSettingsToUse = new ScoutWaveSet[0];
    }

    public class ScoutWaveSet
    {
        public string TargetSetting;
        public string WaveSetting;
    }

    public class ScoutTargetSetting
    {
        public string Name;

        public TargetSetting Target;
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
        public bool TriggerAlarm;
    }
}
