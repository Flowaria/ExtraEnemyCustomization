using ExtraEnemyCustomization.Customizations;
using System.Collections.Generic;
using System.Linq;

namespace ExtraEnemyCustomization.Configs
{
    public class ModelCustomConfigData : ConfigData
    {
        public bool CacheAllMaterials = false;

        public ShadowCustom[] ShadowCustom = new ShadowCustom[0];
        public MaterialCustom[] MaterialCustom = new MaterialCustom[0];
        public LimbCustom[] LimbCustom = new LimbCustom[0];

        public override EnemyCustomBase[] GetAllSettings()
        {
            var list = new List<EnemyCustomBase>();
            list.AddRange(ShadowCustom);
            list.AddRange(MaterialCustom);
            list.AddRange(LimbCustom);
            return list.ToArray();
        }
    }
}
