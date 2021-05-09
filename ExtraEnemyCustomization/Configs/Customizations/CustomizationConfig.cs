using EECustom.Customizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Configs.Customizations
{
    public abstract class CustomizationConfig
    {
        public abstract EnemyCustomBase[] GetAllSettings();
    }
}
