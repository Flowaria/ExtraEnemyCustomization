using EECustom.Customizations.ShooterFires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EECustom.Customizations.Abilities.Managers
{
    public class ShooterDistSettingManager : MonoBehaviour
    {
        public EAB_ProjectileShooter DefaultValue;
        public EAB_ProjectileShooter EAB_Shooter;
        public FireSetting[] FireSettings;

        private FireSetting _currentSetting = null;
        private float _timerToUpdate = 0.0f;

        public ShooterDistSettingManager(IntPtr ptr) : base(ptr)
        {
        }

        internal void Update()
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
                newSetting.ApplyToEAB(EAB_Shooter, DefaultValue);
                _currentSetting = newSetting;
            }
        }
    }
}
