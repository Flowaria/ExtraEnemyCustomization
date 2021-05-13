using EECustom.Customizations;
using EECustom.Customizations.Strikers;
using EECustom.CustomSettings.DTO;
using System.Collections.Generic;

namespace EECustom.Configs.Customizations
{
    public class TentacleCustomConfig : CustomizationConfig
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