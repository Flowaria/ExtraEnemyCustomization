using BepInEx.Logging;
using Enemies;
using GameData;
using System;
using System.Linq;

namespace EECustom.Customizations
{
    public abstract class EnemyCustomBase
    {
        public string DebugName { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public TargetSetting Target { get; set; } = new TargetSetting();

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

        public void LogVerbose(string str)
        {
            LogFormatDebug(str, true);
        }

        public void LogDev(string str)
        {
            LogFormatDebug(str, false);
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

        private void LogFormatDebug(string str, bool verbose)
        {
            string prefix;
            if (!string.IsNullOrEmpty(DebugName))
                prefix = $"[{GetProcessName()}-{DebugName}]";
            else
                prefix = $"[{GetProcessName()}]";

            if (verbose)
                Logger.Verbose($"{prefix} {str}");
            else
                Logger.Debug($"{prefix} {str}");
        }
    }

    public class TargetSetting
    {
        public TargetMode Mode { get; set; } = TargetMode.PersistentID;
        public uint[] persistentIDs { get; set; } = new uint[1] { 0 };
        public string nameParam { get; set; } = string.Empty;
        public bool nameIgnoreCase { get; set; } = false;

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