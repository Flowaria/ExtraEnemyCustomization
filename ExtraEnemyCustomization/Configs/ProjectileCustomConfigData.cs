using ExtraEnemyCustomization.Customizations;
using ExtraEnemyCustomization.Customizations.CustomProjectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.Configs
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
