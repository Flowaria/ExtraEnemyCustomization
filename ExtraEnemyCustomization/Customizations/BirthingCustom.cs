using Enemies;
using GameData;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ExtraEnemyCustomization.Customizations
{
    public class BirthingCustom : EnemyCustomBase
    {
        public uint EnemyGroupToSpawn;
        public float ChildrenCost = 1.0f;
        public int ChildrenPerBirth = 10;
        public int ChildrenPerBirthMin = 3;
        public int ChildrenMax = 20;
        public float MinDelayUntilNextBirth = 1.0f;
        public float MaxDelayUntilNextBirth = 14.0f;


        public override string GetProcessName()
        {
            return "Birthing";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            var eabBirth = agent.GetComponentInChildren<EAB_Birthing>(true);
            if(eabBirth != null)
            {
                eabBirth.m_groupDataBlock = GameDataBlockBase<EnemyGroupDataBlock>.GetBlock(EnemyGroupToSpawn);
                eabBirth.m_childrenCost = ChildrenCost;
                eabBirth.m_childrenPerBirth = ChildrenPerBirth;
                eabBirth.m_childrenAllowedSpawn = ChildrenPerBirth;
                eabBirth.m_childrenPerBirthMin = ChildrenPerBirthMin;
                eabBirth.m_childrenMax = ChildrenMax;
                eabBirth.m_minDelayUntilNextBirth = MinDelayUntilNextBirth;
                eabBirth.m_maxDelayUntilNextBirth = MaxDelayUntilNextBirth;
            }
        }
    }
}
