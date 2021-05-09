using Agents;
using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Events
{
    public static class EnemyDamageEvents
    {
        //public static Action<EnemyAgent, Agent, float> OnDamage;
        public static Action<EnemyAgent, Agent> OnDamage;
    }
}
