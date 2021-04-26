using ExtraEnemyCustomization.Customizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.Configs
{
    public abstract class ConfigData
    {
        public abstract EnemyCustomBase[] GetAllSettings();
    }
}
