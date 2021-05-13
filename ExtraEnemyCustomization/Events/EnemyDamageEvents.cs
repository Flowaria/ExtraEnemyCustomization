using Agents;
using Enemies;
using System;

namespace EECustom.Events
{
    public static class EnemyDamageEvents
    {
        //public static Action<EnemyAgent, Agent, float> OnDamage;
        public static Action<EnemyAgent, Agent> OnDamage;
    }
}