using EECustom.Customizations.Abilities.Managers;
using EECustom.Utils;
using Enemies;

namespace EECustom.Customizations.Abilities
{
    public class HealthRegenCustom : EnemyCustomBase
    {
        public HealthRegenData[] RegenDatas = new HealthRegenData[0];

        public override string GetProcessName()
        {
            return "HealthRegen";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            if (agent.Damage != null)
            {
                foreach (var regenData in RegenDatas)
                {
                    var ability = agent.gameObject.AddComponent<HealthRegenManager>();
                    ability.DamageBase = agent.Damage;
                    ability.RegenData = regenData;
                }
            }
        }
    }

    public class HealthRegenData
    {
        public float RegenInterval = 1.0f;
        public float DelayUntilRegenStart = 5.0f;
        public bool CanDamageInterruptRegen = true;
        public ValueBase RegenAmount = ValueBase.Zero;
        public ValueBase RegenCap = ValueBase.Zero;
    }
}