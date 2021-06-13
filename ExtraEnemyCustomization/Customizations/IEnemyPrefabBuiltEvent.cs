using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Customizations
{
    public interface IEnemyPrefabBuiltEvent
    {
        void OnPrefabBuilt(EnemyAgent agent);
    }
}
