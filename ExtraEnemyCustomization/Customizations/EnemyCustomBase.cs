using BepInEx.Logging;
using Enemies;
using GameData;
using System;
using System.Linq;

namespace ExtraEnemyCustomization.Customizations
{
    public abstract class EnemyCustomBase
    {
        public string DebugName = string.Empty;
        public bool Enabled = true;
        public TargetSetting Target;

        public virtual void Initialize()
        {
        }

        public virtual void Prespawn(EnemyAgent agent)
        {
        }

        public virtual void Postspawn(EnemyAgent agent)
        {
        }

        public abstract string GetProcessName();

        public virtual bool HasPrespawnBody { get { return false; } }
        public virtual bool HasPostspawnBody { get { return false; } }

        public void LogDev(string str)
        {
            LogFormat(LogLevel.Debug, str);
        }

        public void LogError(string str)
        {
            LogFormat(LogLevel.Error, str);
        }

        public void LogWarning(string str)
        {
            LogFormat(LogLevel.Warning, str);
        }

        private void LogFormat(LogLevel level, string str)
        {
            if (!string.IsNullOrEmpty(DebugName))
                Logger.LogInstance.Log(level, $"[{GetProcessName()}-{DebugName}] {str}");
            else
                Logger.LogInstance.Log(level, $"[{GetProcessName()}] {str}");
        }
    }

    public class TargetSetting
    {
        public TargetMode Mode = TargetMode.PersistentID;
        public uint[] persistentIDs = new uint[1] { 0 };
        public string nameParam = string.Empty;
        public bool nameIgnoreCase = false;

        public bool IsMatch(EnemyAgent agent)
        {
            var enemyData = GameDataBlockBase<EnemyDataBlock>.GetBlock(agent.EnemyDataID);
            var comparisonMode = nameIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            return Mode switch
            {
                TargetMode.PersistentID => persistentIDs.Contains(agent.EnemyDataID),
                TargetMode.NameEquals => enemyData?.name?.Equals(nameParam, comparisonMode) ?? false,
                TargetMode.NameContains => enemyData?.name?.Contains(nameParam, comparisonMode) ?? false,
                TargetMode.Everything => true,
                _ => false,
            };
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