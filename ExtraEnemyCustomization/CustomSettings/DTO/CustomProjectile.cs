using EECustom.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EECustom.CustomSettings.DTO
{
    public class CustomProjectile
    {
        public string DebugName = string.Empty;
        public byte ID = 10;
        public ProjectileType BaseProjectile = ProjectileType.TargetingSmall;
        public ValueBase Speed = ValueBase.Unchanged;
        public ValueBase HomingStrength = ValueBase.Unchanged;
        public Color GlowColor = Color.yellow;
        public ValueBase GlowRange = ValueBase.Unchanged;
        public ValueBase Damage = ValueBase.Unchanged;
        public ValueBase Infection = ValueBase.Unchanged;
    }
}