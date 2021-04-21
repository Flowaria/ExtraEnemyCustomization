using Enemies;
using GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraEnemyCustomization.Customizations
{
    public abstract class EnemyCustomBase
    {
        public bool Enabled = true;
        public TargetSetting Target;
        public virtual void Prespawn(EnemyAgent agent) { }
        public virtual void Postspawn(EnemyAgent agent) { }
        public abstract string GetProcessName();

        public virtual bool HasPrespawnBody { get { return false; } }
        public virtual bool HasPostspawnBody { get { return false; } }

        public void LogDev(string str)
        {
            Logger.Debug($"[{GetProcessName()}] {str}");
        }

        public void LogError(string str)
        {
            Logger.Error($"[{GetProcessName()}] {str}");
        }

        public void LogWarning(string str)
        {
            Logger.Warning($"[{GetProcessName()}] {str}");
        }
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
