using EECustom.Customizations;
using EECustom.Customizations.ShooterFires;
using EECustom.CustomSettings.CustomProjectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Configs
{
    public class ProjectileCustomConfigData : ConfigData
    {
        public ShooterFireCustom[] ShooterFireCustom = new ShooterFireCustom[0];
        public CustomProjectile[] ProjectileDefinitions = new CustomProjectile[0];

        public override EnemyCustomBase[] GetAllSettings()
        {
            var list = new List<EnemyCustomBase>();
            list.AddRange(ShooterFireCustom);
            return list.ToArray();
        }
    }
}
