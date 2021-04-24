using Enemies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if(projectileSetting != null)
            {
                if(FireSettings.Length > 1)
                {
                    var mgr = agent.gameObject.AddComponent<ShooterDistanceConfigManager>();
                    mgr.EAB_Shooter = projectileSetting;

                    if(_SortedFireSettings == null)
                    {
                        _SortedFireSettings = FireSettings.OrderByDescending(f => f.FromDistance).ToArray();
                    }
                    mgr.FireSettings = _SortedFireSettings;
                }
                else
                {
                    FireSettings[0].ApplyToEAB(projectileSetting);
                }
            }
        }
    }

    public class FireSetting
    {
        public float FromDistance = -1.0f;

        public ProjectileType ProjectileType = ProjectileType.TargetingLarge;
        public int BurstCount = 20;
        public float BurstDelay = 3.0f;
        public float ShotDelayMin = 0.05f;
        public float ShotDelayMax = 0.3f;
        public float InitialFireDelay = 0.0f;
        public float ShotSpreadXMin = -45.0f;
        public float ShotSpreadXMax = 45.0f;
        public float ShotSpreadYMin = -20.0f;
        public float ShotSpreadYMax = 20.0f;

        public void ApplyToEAB(EAB_ProjectileShooter eab)
        {
            eab.m_type = ProjectileType;
            eab.m_burstCount = BurstCount;
            eab.m_burstDelay = BurstDelay;
            eab.m_shotDelayMin = ShotDelayMin;
            eab.m_shotDelayMax = ShotDelayMax;
            eab.m_initialFireDelay = InitialFireDelay;
            eab.m_shotSpreadX = new Vector2(ShotSpreadXMin, ShotSpreadXMax);
            eab.m_shotSpreadY = new Vector2(ShotSpreadYMin, ShotSpreadYMax);
        }
    }

    public class ShooterDistanceConfigManager : MonoBehaviour
    {
        public EAB_ProjectileShooter EAB_Shooter;
        public FireSetting[] FireSettings;

        private FireSetting _currentSetting = null;
        private float _timerToUpdate = 0.0f;
        private float _lastSettingFromDistance = 0.0f;

        public ShooterDistanceConfigManager(IntPtr ptr) : base(ptr)
        {
        }

        private void Update()
        {
            if (Clock.Time < _timerToUpdate)
                return;

            _timerToUpdate = Clock.Time + 0.5f;

            if (!EAB_Shooter.m_owner.AI.IsTargetValid)
                return;

            var distance = EAB_Shooter.m_owner.AI.Target.m_distance;
            var newSetting = FireSettings.FirstOrDefault(x => x.FromDistance <= distance);
            if (newSetting != _currentSetting)
            {
                newSetting.ApplyToEAB(EAB_Shooter);
                _currentSetting = newSetting;
            }
        }
    }
}
