using EECustom.Customizations;
using EECustom.Customizations.Detections;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Configs.Customizations
{
    public class DetectionCustomConfig : CustomizationConfig
    {
        public ScreamingCustom[] ScreamingCustom = new ScreamingCustom[0];
        public FeelerCustom[] FeelerCustom = new FeelerCustom[0];

        public override EnemyCustomBase[] GetAllSettings()
        {
            var list = new List<EnemyCustomBase>();
            list.AddRange(ScreamingCustom);
            list.AddRange(FeelerCustom);
            return list.ToArray();
        }
    }
}
