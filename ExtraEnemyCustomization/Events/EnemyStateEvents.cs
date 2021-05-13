using Enemies;
using System;

namespace EECustom.Events
{
    public static class EnemyStateEvents
    {
        public static Func<EnemyAgent, ES_StateEnum, bool> OnActivateState;
    }
}