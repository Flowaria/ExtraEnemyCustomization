using EECustom.Events;
using Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EECustom.Customizations.Models
{
    public class GlowCustom : EnemyCustomBase
    {
        public Color DefaultColor { get; set; } = Color.black;
        
        public Color HibernateColor { get; set; } = new Vector4(9f, 3.9f, 3f, 1f);
        public Color HeartbeatColor { get; set; } = new Vector4(9f, 3.9f, 3f, 1f);
        public Color SelfWakeupColor { get; set; } = new Vector4(3f, 0.2f, 0.2f, 0.5f) * 3f;
        public Color PropagateWakeupColor { get; set; } = new Vector4(3f, 0.2f, 0.2f, 0.5f) * 3f;

        public Color TentacleAttackColor { get; set; } = new Vector4(1.5f, 0.1f, 0.1f, 1f) * 1.75f;
        public Color ShooterFireColor { get; set; } = new Vector4(1f, 0.5f, 0.45f, 1f) * 2.15f;
        

        public override string GetProcessName()
        {
            return "Glow";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            //Those are fine
            agent.MaterialHandler.m_defaultGlowColor = DefaultColor;
            agent.Locomotion.ShooterAttack.m_attackGlowColor_End = ShooterFireColor;
            agent.Locomotion.StrikerAttack.m_attackGlowColor = TentacleAttackColor;
            agent.Locomotion.TankMultiTargetAttack.m_attackGlowColor = TentacleAttackColor;
            agent.Locomotion.HibernateWakeup.m_selfWakeupColor = SelfWakeupColor;
            agent.Locomotion.Hibernate.m_heartbeatColorVec = HeartbeatColor;

            //And this is static LMAO
            //ES_HibernateWakeUp.m_propagatedWakeupColor = PropagateWakeupColor
            //ES_Hibernate.s_closestDistanceDetectionColorVec = HibernateColor
        }
    }
}
