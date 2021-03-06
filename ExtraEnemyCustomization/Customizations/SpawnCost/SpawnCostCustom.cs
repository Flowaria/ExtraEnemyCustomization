using Agents;
using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations.SpawnCost
{
    public class SpawnCostCustom : EnemyCustomBase, IEnemySpawnedEvent
    {
        public float SpawnCost { get; set; } = 0.0f;
        public override string GetProcessName()
        {
            return "SpawnCost";
        }

        public void OnSpawned(EnemyAgent agent)
        {
            agent.m_enemyCost = SpawnCost;
            if (agent.AI.Mode == AgentMode.Agressive)
            {
                float delta = EnemyCostManager.Current.m_enemyTypeCosts[(int)agent.EnemyData.EnemyType] - SpawnCost;
                EnemyCostManager.AddCost(-delta);
                LogDev($"Decremented cost by {delta}!");
            }
            else
            {
                LogDev($"Set Enemy Cost to {SpawnCost}!");
            }
        }
    }
}
