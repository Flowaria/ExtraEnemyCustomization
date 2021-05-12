using AIGraph;
using EECustom.CustomSettings.DTO;
using Enemies;
using GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EECustom.CustomSettings
{
    public static class CustomScoutWaveManager
    {
        private static List<ExpeditionScoutSetting> _ScoutSettings = new List<ExpeditionScoutSetting>();
        private static List<ScoutSettingCache> _ActiveScoutSettings = new List<ScoutSettingCache>();
        private static Dictionary<string, ScoutTargetSetting> _TargetSettingDict = new Dictionary<string, ScoutTargetSetting>();
        private static Dictionary<string, ScoutWaveSetting> _WaveSettingDict = new Dictionary<string, ScoutWaveSetting>();

        private static ExpeditionData _PreviousExpData = null;
        private static uint _DefaultWaveSettingID;
        private static uint _DefaultWavePopulationID;
        private static eRundownTier _CurrentExpeditionTier;
        private static int _CurrentExpeditionIndex;
        private static System.Random _Random = new System.Random();

        public static void ExpeditionUpdate(eRundownTier tier, int expIndex, ExpeditionInTierData inTierData)
        {
            Logger.Warning("EXP UPDATED1");

            //Revert Previous Expedition Data
            if (_PreviousExpData != null)
            {
                _PreviousExpData.ScoutWaveSettings = _DefaultWaveSettingID;
                _PreviousExpData.ScoutWavePopulation = _DefaultWavePopulationID;
            }

            _PreviousExpData = inTierData.Expedition;
            _DefaultWaveSettingID = _PreviousExpData.ScoutWaveSettings;
            _DefaultWavePopulationID = _PreviousExpData.ScoutWavePopulation;

            _CurrentExpeditionTier = tier;
            _CurrentExpeditionIndex = expIndex;

            _ActiveScoutSettings.Clear();
            foreach (var setting in _ScoutSettings)
            {
                if (!setting.IsMatch(_CurrentExpeditionTier, _CurrentExpeditionIndex))
                    continue;

                foreach(var scoutSetting in setting.ScoutSettings)
                {
                    var targetSetting = GetTargetSetting(scoutSetting.TargetSetting);
                    var waveSetting = GetWaveSetting(scoutSetting.WaveSetting);

                    if (targetSetting == null)
                        continue;

                    if (waveSetting == null)
                        continue;

                    var settingCache = new ScoutSettingCache()
                    {
                        TargetSetting = targetSetting,
                        WaveSetting = waveSetting
                    };
                    _ActiveScoutSettings.Add(settingCache);
                }
            }

            Logger.Warning("EXP UPDATED");
        }

        public static void AddScoutSetting(params ExpeditionScoutSetting[] scoutSettings)
        {
            foreach (var setting in scoutSettings)
                AddScoutSetting(setting);
        }

        public static void AddScoutSetting(ExpeditionScoutSetting scoutSetting)
        {
            _ScoutSettings.Add(scoutSetting);
        }

        public static void AddWaveSetting(params ScoutWaveSetting[] waveSettings)
        {
            foreach (var setting in waveSettings)
                AddWaveSetting(setting);
        }

        public static void AddWaveSetting(ScoutWaveSetting waveSetting)
        {
            var key = waveSetting.Name.ToLower();
            if (!_WaveSettingDict.ContainsKey(key))
            {
                _WaveSettingDict.Add(waveSetting.Name, waveSetting);
            }
        }

        public static ScoutWaveSetting GetWaveSetting(string name)
        {
            var key = name.ToLower();
            if (_WaveSettingDict.ContainsKey(key))
                return _WaveSettingDict[key];
            else
                return null;
        }

        public static void AddTargetSetting(params ScoutTargetSetting[] targetSettings)
        {
            foreach (var setting in targetSettings)
                AddTargetSetting(setting);
        }

        public static void AddTargetSetting(ScoutTargetSetting targetSetting)
        {
            var key = targetSetting.Name.ToLower();
            if (!_TargetSettingDict.ContainsKey(key))
            {
                _TargetSettingDict.Add(targetSetting.Name, targetSetting);
            }
        }

        public static ScoutTargetSetting GetTargetSetting(string name)
        {
            var key = name.ToLower();
            if (_TargetSettingDict.ContainsKey(key))
                return _TargetSettingDict[key];
            else
                return null;
        }

        public static void TriggerScoutWave(EnemyAgent scoutAgent)
        {
            bool triggeredAny = false;
            foreach (var scoutSetting in _ActiveScoutSettings)
            {
                var targetSetting = scoutSetting.TargetSetting;
                var waveSetting = scoutSetting.WaveSetting;

                if (!targetSetting.Target.IsMatch(scoutAgent))
                    continue;

                if (waveSetting.Waves.GetLength(0) <= 0)
                    continue;

                var waveVariantCount = waveSetting.Waves.GetLength(0);
                if (waveVariantCount > 1)
                {
                    var weightValues = new float[waveVariantCount];
                    if (waveSetting.WeightsOverride.Length > 0)
                    {
                        for (int i = 0; i < waveSetting.WeightsOverride.Length; i++)
                        {
                            weightValues[i] = Math.Min(0.0f, waveSetting.WeightsOverride[i]);
                        }
                    }

                    var index = PickWeightedRandom(weightValues);
                    TriggerWaves(waveSetting.Waves[index], scoutAgent.CourseNode);
                    triggeredAny = true;
                }
                else if (waveVariantCount == 1)
                {
                    TriggerWaves(waveSetting.Waves[0], scoutAgent.CourseNode);
                    triggeredAny = true;
                }
                else
                {
                    Logger.Warning("WaveSetting count was zero: Wave has ignored");
                }
            }

            if (!triggeredAny)
            {
                TriggerDefaultWave(scoutAgent.CourseNode);
            }
        }

        private static int PickWeightedRandom(float[] weights)
        {
            if (weights.Length <= 0)
                throw new ArgumentException("weights length is zero!");

            if (weights.Length == 1)
                return 0;

            var weightValues = new float[weights.Length];
            weights.CopyTo(weightValues, 0);

            var weightsTotal = weightValues.Sum();
            if (weightsTotal <= 0.0f)
            {
                return _Random.Next(0, weights.Length);
            }

            var accumulatedWeight = 0.0f;
            for (int i = 0; i < weightValues.Length; i++)
            {
                if (weightValues[i] <= 0.0f)
                {
                    weightValues[i] = -1.0f;
                    continue;
                }

                accumulatedWeight += weightValues[i];
                weightValues[i] = accumulatedWeight;
            }

            var randValue = (float)_Random.NextDouble() * weightsTotal;
            for (int i = 0; i < weightValues.Length; i++)
            {
                if (weightValues[i] >= randValue)
                {
                    return i;
                }
            }

            throw new IndexOutOfRangeException("index was not picked?!");
        }

        private static void TriggerWaves(WaveSetting[] waves, AIG_CourseNode sourceNode)
        {
            foreach(var wave in waves)
            {
                Mastermind.Current.TriggerSurvivalWave(
                    refNode:            sourceNode,
                    settingsID:         wave.WaveSettingID,
                    populationDataID:   wave.WavePopulationID,
                    eventID:            out _,
                    spawnType:          SurvivalWaveSpawnType.InRelationToClosestAlivePlayer,
                    spawnDelay:         wave.Delay,
                    playScreamOnSpawn:  true
                    );
            }
        }

        private static void TriggerDefaultWave(AIG_CourseNode sourceNode)
        {
            Mastermind.Current.TriggerSurvivalWave(
                    refNode: sourceNode,
                    settingsID: _DefaultWaveSettingID,
                    populationDataID: _DefaultWavePopulationID,
                    eventID: out _,
                    spawnType: SurvivalWaveSpawnType.InRelationToClosestAlivePlayer,
                    spawnDelay: 0.0f,
                    playScreamOnSpawn: true
                    );
        }
    }

    public struct ScoutSettingCache
    {
        public ScoutTargetSetting TargetSetting;
        public ScoutWaveSetting WaveSetting;
    }
}
