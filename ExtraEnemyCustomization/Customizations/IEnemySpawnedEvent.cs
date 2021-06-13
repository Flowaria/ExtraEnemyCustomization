using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations
{
    public interface IEnemySpawnedEvent
    {
        void OnSpawned(EnemyAgent agent);
    }
}
