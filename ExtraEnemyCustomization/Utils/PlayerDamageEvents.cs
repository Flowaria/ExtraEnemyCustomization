using Agents;
using Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Utils
{
    public static class PlayerDamageEvents
    {
        public static Action<PlayerAgent, Agent, float> OnDamage = null;
        public static Action<PlayerAgent, Agent, float> OnMeleeDamage = null;
        public static Action<PlayerAgent, Agent, float> OnTentacleDamage = null;
    }
}
