using GameData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.CustomSettings
{
    public static class CustomScoutWaveManager
    {
        private static ExpeditionData _PreviousExpData = null;
        private static uint _DefaultWaveSettingID;
        private static uint _DefaultWavePopulationID;
        private static int _CurrentExpeditionTier;
        private static int _CurrentExpeditionIndex;

        static CustomScoutWaveManager()
        {
            RundownManager.add_OnExpeditionUpdated(new Action<pActiveExpedition, ExpeditionInTierData>(OnExpeditionUpdated));
        }

        private static void OnExpeditionUpdated(pActiveExpedition activeExp, ExpeditionInTierData inTierData)
        {
            //Revert Previous Expedition Data
            if(_PreviousExpData != null)
            {
                _PreviousExpData.ScoutWaveSettings = _DefaultWaveSettingID;
                _PreviousExpData.ScoutWavePopulation = _DefaultWavePopulationID;
            }

            _PreviousExpData = inTierData.Expedition;
            _DefaultWaveSettingID = _PreviousExpData.ScoutWaveSettings;
            _DefaultWavePopulationID = _PreviousExpData.ScoutWavePopulation;

            _CurrentExpeditionTier = (int)activeExp.tier;
            _CurrentExpeditionIndex = activeExp.expeditionIndex;
        }
    }
}
