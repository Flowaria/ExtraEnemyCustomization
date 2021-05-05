using Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Events
{
    public static class EnemyStateEvents
    {
        public static Func<EnemyAgent, ES_StateEnum, bool> OnActivateState;
    }
}
