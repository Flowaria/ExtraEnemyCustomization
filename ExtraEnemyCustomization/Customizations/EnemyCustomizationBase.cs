using Enemies;
using GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraEnemyCustomization.Customizations
{
    public abstract class EnemyCustomizationBase
    {
        public bool Enabled = true;
        public TargetSetting Target;
        public virtual void PreSpawn(EnemyAgent agent) { }
        public virtual void PostSpawn(EnemyAgent agent) { }
    }

    public class TargetSetting
    {
        public TargetMode Mode = TargetMode.PersistentID;
        public uint[] persistentIDs = new uint[1] { 0 };
        public string nameParam = string.Empty;

        public bool IsMatch(EnemyAgent agent)
        {
            var enemyData = GameDataBlockBase<EnemyDataBlock>.GetBlock(agent.EnemyDataID);

            switch (Mode)
            {
                case TargetMode.PersistentID:
                    return persistentIDs.Contains(agent.EnemyDataID);

                case TargetMode.NameEquals:
                    return enemyData?.name?.Equals(nameParam) ?? false;

                case TargetMode.NameContains:
                    return enemyData?.name?.Contains(nameParam) ?? false;

                case TargetMode.Everything:
                    return true;

                default:
                    return false;
            }
        }

        public string ToDebugString()
        {
            return $"TargetDebug, Mode: {Mode}, persistentIDs: [{string.Join(", ", persistentIDs)}], nameParam: {nameParam}";
        }
    }

    public enum TargetMode
    {
        PersistentID,
        NameEquals,
        NameContains,
        Everything
    }
}
