using Agents;
using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Utils
{
    public static class EnemyDamageEvents
    {
        public static Action<EnemyAgent, Agent, float> OnDamage;
    }
}
