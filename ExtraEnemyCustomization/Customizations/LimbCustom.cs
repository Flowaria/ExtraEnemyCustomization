using Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraEnemyCustomization.Customizations
{
    public class LimbCustom : EnemyCustomBase
    {
        public List<LimbData> Limbs = new List<LimbData>();

        public override void PostSpawn(EnemyAgent agent)
        {
            Logger.DevMessage($"Trying to Apply LimbCustom to Enemy: {agent.name}");

            var allLimbData = Limbs.SingleOrDefault(x => x.LimbName.Equals("All", StringComparison.OrdinalIgnoreCase));

            foreach (var limb in agent.Damage.DamageLimbs)
            {
                Logger.DevMessage($" - Found Limb: {limb.name}");

                var limbCustomData = Limbs.SingleOrDefault(x=>x.LimbName.Equals(limb.name));
                if(limbCustomData == null)
                {
                    if(allLimbData == null)
                    {
                        continue;
                    }
                    limbCustomData = allLimbData;
                }

                Logger.DevMessage($" - Applying Setting to Limb, LimbType: {limbCustomData.LimbType}, CustomMult: {limbCustomData.CustomMulti}, HealthValueMode: {limbCustomData.HealthValueMode}, HealthValue: {limbCustomData.HealthValue}");

                limb.m_healthMax *= limbCustomData.HealthValue;
                limb.m_health *= limbCustomData.HealthValue;

                var isCustom = (limbCustomData.LimbType == LimbDamageType.ArmorCustom || limbCustomData.LimbType == LimbDamageType.WeakspotCustom);
                var healthData = agent.EnemyBalancingData.Health;
                switch (limbCustomData.LimbType)
                {
                    case LimbDamageType.Normal:
                        limb.m_armorDamageMulti = 1.0f;
                        limb.m_weakspotDamageMulti = 1.0f;
                        limb.m_type = eLimbDamageType.Normal;
                        break;

                    case LimbDamageType.Armor:
                    case LimbDamageType.ArmorCustom:
                        limb.m_type = eLimbDamageType.Armor;
                        limb.m_weakspotDamageMulti = 1.0f;
                        limb.m_armorDamageMulti = isCustom ? limbCustomData.CustomMulti : healthData.ArmorDamageMulti;
                        break;

                    case LimbDamageType.Weakspot:
                    case LimbDamageType.WeakspotCustom:
                        limb.m_type = eLimbDamageType.Weakspot;
                        limb.m_armorDamageMulti = 1.0f;
                        limb.m_weakspotDamageMulti = isCustom ? limbCustomData.CustomMulti : healthData.WeakspotDamageMulti;
                        break;
                }
            }
        }
    }

    public class LimbData
    {
        public string LimbName = "Head";
        public LimbDamageType LimbType = LimbDamageType.Weakspot;
        public float CustomMulti = 1.0f;
        public HealthValueMode HealthValueMode = HealthValueMode.Rel;
        public float HealthValue = 1.0f;
    }

    public enum LimbDamageType
    {
        Normal,
        Weakspot,
        WeakspotCustom,
        Armor,
        ArmorCustom
    }

    public enum HealthValueMode
    {
        Rel, Abs
    }
}
