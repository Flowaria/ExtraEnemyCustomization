using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Events
{
    public static class ScoutAntennaSpawnEvent
    {
        public static Action<EnemyAgent, ScoutAntennaDetection> OnDetectionSpawn;
        public static Action<EnemyAgent, ScoutAntennaDetection, ScoutAntenna> OnAntennaSpawn;
    }
}
