using Enemies;
using ExtraEnemyCustomization.Customizations.Abilities;
using ExtraEnemyCustomization.Utils;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine;

namespace ExtraEnemyCustomization.Customizations
{
    public class ShooterFireCustom : EnemyCustomBase
    {
        public FireSetting[] FireSettings = new FireSetting[0];

        [JsonIgnore]
        private FireSetting[] _SortedFireSettings = null;

        public override string GetProcessName()
        {
            return "ShooterFire";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            var projectileSetting = agent.GetComponentInChildren<EAB_ProjectileShooter>(true);
            if (projectileSetting != null)
            {
                if (FireSettings.Length > 1)
                {
                    var clone = new EAB_ProjectileShooter()
                    {
                        m_burstCount = projectileSetting.m_burstCount,
                        m_burstDelay = projectileSetting.m_burstDelay,
                        m_shotDelayMin = projectileSetting.m_shotDelayMin,
                        m_shotDelayMax = projectileSetting.m_shotDelayMax,
                        m_initialFireDelay = projectileSetting.m_initialFireDelay,
                        m_shotSpreadX = projectileSetting.m_shotSpreadX,
                        m_shotSpreadY = projectileSetting.m_shotSpreadY
                    };

                    var ability = agent.gameObject.AddComponent<ShooterDistSettingAbility>();
                    ability.DefaultValue = clone;
                    ability.EAB_Shooter = projectileSetting;

                    if (_SortedFireSettings == null)
                    {
                        _SortedFireSettings = FireSettings.OrderByDescending(f => f.FromDistance).ToArray();
                    }
                    ability.FireSettings = _SortedFireSettings;
                }
                else if (FireSettings.Length == 1)
                {
                    FireSettings[0].ApplyToEAB(projectileSetting);
                }
            }
        }
    }

    public class FireSetting
    {
        public float FromDistance = -1.0f;

        public bool OverrideProjectileType = true;
        public ProjectileType ProjectileType = ProjectileType.TargetingLarge;
        public ValueBase BurstCount = ValueBase.Unchanged;
        public ValueBase BurstDelay = ValueBase.Unchanged;
        public ValueBase ShotDelayMin = ValueBase.Unchanged;
        public ValueBase ShotDelayMax = ValueBase.Unchanged;
        public ValueBase InitialFireDelay = ValueBase.Unchanged;
        public ValueBase ShotSpreadXMin = ValueBase.Unchanged;
        public ValueBase ShotSpreadXMax = ValueBase.Unchanged;
        public ValueBase ShotSpreadYMin = ValueBase.Unchanged;
        public ValueBase ShotSpreadYMax = ValueBase.Unchanged;

        public void ApplyToEAB(EAB_ProjectileShooter eab, EAB_ProjectileShooter defValue = null)
        {
            if (OverrideProjectileType)
                eab.m_type = ProjectileType;

            if (defValue == null)
            {
                defValue = eab;
            }

            eab.m_burstCount = BurstCount.GetAbsValue(defValue.m_burstCount);
            eab.m_burstDelay = BurstDelay.GetAbsValue(defValue.m_burstDelay);
            eab.m_shotDelayMin = ShotDelayMin.GetAbsValue(defValue.m_shotDelayMin);
            eab.m_shotDelayMax = ShotDelayMax.GetAbsValue(defValue.m_shotDelayMax);
            eab.m_initialFireDelay = InitialFireDelay.GetAbsValue(defValue.m_initialFireDelay);
            eab.m_shotSpreadX = new Vector2(ShotSpreadXMin.GetAbsValue(defValue.m_shotSpreadX.x), ShotSpreadXMax.GetAbsValue(defValue.m_shotSpreadX.y));
            eab.m_shotSpreadY = new Vector2(ShotSpreadYMin.GetAbsValue(defValue.m_shotSpreadY.x), ShotSpreadYMax.GetAbsValue(defValue.m_shotSpreadY.y));
        }
    }
}