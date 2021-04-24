using Enemies;

namespace ExtraEnemyCustomization.Customizations
{
    public class StrikerTentacleCustom : EnemyCustomBase
    {
        public GPUCurvyType[] TentacleTypes = new GPUCurvyType[0];

        public override string GetProcessName()
        {
            return "TentacleType";
        }

        public override bool HasPrespawnBody => true;

        public override void Prespawn(EnemyAgent agent)
        {
            var tentacleComps = agent.GetComponentsInChildren<MovingEnemyTentacleBase>(true);
            for (int i = 0; i < tentacleComps.Length; i++)
            {
                var newType = TentacleTypes[i % TentacleTypes.Length];
                tentacleComps[i].m_GPUCurvyType = newType;

                LogDev($" - Applied Tentacle Type!, index: {i} type: {newType.ToString()}");
            }
        }
    }
}