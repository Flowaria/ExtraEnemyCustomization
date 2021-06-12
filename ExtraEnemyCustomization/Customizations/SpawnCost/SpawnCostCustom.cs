using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations.SpawnCost
{
    public class SpawnCostCustom : EnemyCustomBase
    {
        public float SpawnCost { get; set; } = 0.0f;
        public override string GetProcessName()
        {
            return "SpawnCost";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            agent.m_enemyCost = SpawnCost;
            LogDev($"Set Enemy Cost to {SpawnCost}!");
        }
    }
}
