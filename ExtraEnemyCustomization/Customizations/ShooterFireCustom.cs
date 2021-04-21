using Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ExtraEnemyCustomization.Customizations
{
    public class ShooterFireCustom : EnemyCustomBase
    {
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


        public override void PostSpawn(EnemyAgent agent)
        {
            var projectileSetting = agent.GetComponentInChildren<EAB_ProjectileShooter>(true);
            if(projectileSetting != null)
            {
                projectileSetting.m_type = ProjectileType;
                projectileSetting.m_burstCount = BurstCount;
                projectileSetting.m_burstDelay = BurstDelay;
                projectileSetting.m_shotDelayMin = ShotDelayMin;
                projectileSetting.m_shotDelayMax = ShotDelayMax;
                projectileSetting.m_initialFireDelay = InitialFireDelay;
                projectileSetting.m_shotSpreadX = new Vector2(ShotSpreadXMin, ShotSpreadXMax);
                projectileSetting.m_shotSpreadY = new Vector2(ShotSpreadYMin, ShotSpreadYMax);
            }
        }
    }
}
