using EECustom.Utils;
using Enemies;
using GameData;

namespace EECustom.Customizations.Abilities
{
    public class BirthingCustom : EnemyCustomBase
    {
        public uint EnemyGroupToSpawn;
        public ValueBase ChildrenCost = ValueBase.Unchanged;
        public ValueBase ChildrenPerBirth = ValueBase.Unchanged;
        public ValueBase ChildrenPerBirthMin = ValueBase.Unchanged;
        public ValueBase ChildrenMax = ValueBase.Unchanged;
        public ValueBase MinDelayUntilNextBirth = ValueBase.Unchanged;
        public ValueBase MaxDelayUntilNextBirth = ValueBase.Unchanged;

        public override string GetProcessName()
        {
            return "Birthing";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            var eabBirth = agent.GetComponentInChildren<EAB_Birthing>(true);
            if (eabBirth != null)
            {
                eabBirth.m_groupDataBlock = GameDataBlockBase<EnemyGroupDataBlock>.GetBlock(EnemyGroupToSpawn);
                eabBirth.m_childrenCost = ChildrenCost.GetAbsValue(eabBirth.m_childrenCost);
                eabBirth.m_childrenPerBirth = ChildrenPerBirth.GetAbsValue(eabBirth.m_childrenPerBirth);
                eabBirth.m_childrenAllowedSpawn = ChildrenPerBirth.GetAbsValue(eabBirth.m_childrenAllowedSpawn);
                eabBirth.m_childrenPerBirthMin = ChildrenPerBirthMin.GetAbsValue(eabBirth.m_childrenPerBirthMin);
                eabBirth.m_childrenMax = ChildrenMax.GetAbsValue(eabBirth.m_childrenMax);
                eabBirth.m_minDelayUntilNextBirth = MinDelayUntilNextBirth.GetAbsValue(eabBirth.m_minDelayUntilNextBirth);
                eabBirth.m_maxDelayUntilNextBirth = MaxDelayUntilNextBirth.GetAbsValue(eabBirth.m_maxDelayUntilNextBirth);
            }
        }
    }
}