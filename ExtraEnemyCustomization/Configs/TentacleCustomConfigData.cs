using ExtraEnemyCustomization.Customizations;
using ExtraEnemyCustomization.Customizations.CustomTentacles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.Configs
{
    public class TentacleCustomConfigData : ConfigData
    {
        public StrikerTentacleCustom[] StrikerTentacleCustom = new StrikerTentacleCustom[0];
        public CustomTentacle[] TentacleDefinitions = new CustomTentacle[0];

        public override EnemyCustomBase[] GetAllSettings()
        {
            var list = new List<EnemyCustomBase>();
            list.AddRange(StrikerTentacleCustom);
            return list.ToArray();
        }
    }
}
