using EECustom.Customizations;
using EECustom.Customizations.Shooters;
using EECustom.Customizations.SpawnCost;
using EECustom.CustomSettings.DTO;
using System.Collections.Generic;

namespace EECustom.Configs.Customizations
{
    public class SpawnCostCustomConfig : CustomizationConfig
    {
        public SpawnCostCustom[] SpawnCostCustom { get; set; } = new SpawnCostCustom[0];
        public override EnemyCustomBase[] GetAllSettings()
        {
            var list = new List<EnemyCustomBase>();
            list.AddRange(SpawnCostCustom);
            return list.ToArray();
        }
    }
}