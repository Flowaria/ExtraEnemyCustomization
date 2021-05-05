using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Utils
{
    public static class ScoutAntennaSpawnEvent
    {
        public static Action<EnemyAgent, ScoutAntennaDetection> OnDetectionSpawn;
        public static Action<EnemyAgent, ScoutAntennaDetection, ScoutAntenna> OnAntennaSpawn;
    }
}
