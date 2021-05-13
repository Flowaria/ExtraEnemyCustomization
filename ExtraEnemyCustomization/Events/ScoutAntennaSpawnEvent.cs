using Enemies;
using System;

namespace EECustom.Events
{
    public static class ScoutAntennaSpawnEvent
    {
        public static Action<EnemyAgent, ScoutAntennaDetection> OnDetectionSpawn;
        public static Action<EnemyAgent, ScoutAntennaDetection, ScoutAntenna> OnAntennaSpawn;
    }
}