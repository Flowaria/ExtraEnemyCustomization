using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraEnemyCustomization.Customizations
{
    public class StrikerTentacleCustomization : EnemyCustomizationBase
    {
        public GPUCurvyType[] TentacleTypes = new GPUCurvyType[0];

        public override void PreSpawn(EnemyAgent agent)
        {
            Logger.DevMessage($"[TentacleCustom] Applying Tentacle Type to {agent.name}");

            var tentacleComps = agent.GetComponentsInChildren<MovingEnemyTentacleBase>(true);
            for (int i = 0; i < tentacleComps.Length; i++)
            {
                var newType = TentacleTypes[i % TentacleTypes.Length];
                tentacleComps[i].m_GPUCurvyType = newType;

                Logger.DevMessage($"[TentacleCustom] - Applied Tentacle Type!, index: {i} type: {newType.ToString()}");
            }

            Logger.DevMessage("[TentacleCustom] Applied!");
        }
    }
}
