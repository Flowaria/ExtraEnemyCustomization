using EECustom.Customizations;
using EECustom.Customizations.Scouts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Configs
{
    public class ScoutCustomConfigData : ConfigData
    {
        public FeelerCustom[] FeelerCustom = new FeelerCustom[0];

        public override EnemyCustomBase[] GetAllSettings()
        {
            var list = new List<EnemyCustomBase>();
            list.AddRange(FeelerCustom);
            return list.ToArray();
        }
    }
}
