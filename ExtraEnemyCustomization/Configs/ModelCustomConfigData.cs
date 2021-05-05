using EECustom.Customizations;
using EECustom.Customizations.Models;
using System.Collections.Generic;
using System.Linq;

namespace EECustom.Configs
{
    public class ModelCustomConfigData : ConfigData
    {
        public bool CacheAllMaterials = false;

        public ShadowCustom[] ShadowCustom = new ShadowCustom[0];
        public MaterialCustom[] MaterialCustom = new MaterialCustom[0];
        public LimbCustom[] LimbCustom = new LimbCustom[0];
        public ModelRefCustom[] ModelRefCustom = new ModelRefCustom[0];
        public MarkerCustom[] MarkerCustom = new MarkerCustom[0];

        public override EnemyCustomBase[] GetAllSettings()
        {
            var list = new List<EnemyCustomBase>();
            list.AddRange(ShadowCustom);
            list.AddRange(MaterialCustom);
            list.AddRange(LimbCustom);
            list.AddRange(ModelRefCustom);
            list.AddRange(MarkerCustom);
            return list.ToArray();
        }
    }
}
