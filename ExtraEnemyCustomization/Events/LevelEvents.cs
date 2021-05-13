using System;

namespace EECustom.Events
{
    public static class LevelEvents
    {
        public static Action OnBuildStart;
        public static Action OnBuildDone;
        public static Action OnLevelCleanup;
    }
}