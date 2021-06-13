using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations
{
    public interface IEnemyDespawnedEvent
    {
        void OnDespawned(EnemyAgent agent);
    }
}
