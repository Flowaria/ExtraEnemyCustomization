using Agents;
using Enemies;
using EECustom.Utils;
using Player;
using System;
using System.Collections.Generic;
using System.Text;
using EECustom.Events;

namespace EECustom.Customizations.Abilities
{
    public class InfectionAttackCustom : EnemyCustomBase
    {
        public InfectionAttackData MeleeData = new InfectionAttackData();
        public InfectionAttackData TentacleData = new InfectionAttackData();

        private readonly List<ushort> _EnemyList = new List<ushort>();

        public override string GetProcessName()
        {
            return "InfectionAttack";
        }

        public override void Initialize()
        {
            PlayerDamageEvents.OnMeleeDamage += OnMelee;
            PlayerDamageEvents.OnTentacleDamage += OnTentacle;
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            var id = agent.GlobalID;
            if (id == ushort.MaxValue)
                return;

            if (!_EnemyList.Contains(id))
            {
                _EnemyList.Add(id);
                agent.add_OnDeadCallback(new Action(() => { OnDead(id); }));
            }
        }

        public void OnMelee(PlayerAgent player, Agent inflictor, float damage)
        {
            if (_EnemyList.Contains(inflictor.GlobalID))
            {
                ApplyInfection(MeleeData, player, inflictor);
            }
        }

        public void OnTentacle(PlayerAgent player, Agent inflictor, float damage)
        {
            if (_EnemyList.Contains(inflictor.GlobalID))
            {
                ApplyInfection(TentacleData, player, inflictor);
            }
        }

        public void ApplyInfection(InfectionAttackData data, PlayerAgent player, Agent _)
        {
            var infectionAbs = data.Infection.GetAbsValue(PlayerData.MaxInfection);
            if (infectionAbs == 0.0f)
                return;

            if (data.SoundEventID != 0u)
            {
                player.Sound.Post(data.SoundEventID);
            }

            if (data.UseEffect)
            {
                var liquidSetting = ScreenLiquidSettingName.spitterJizz;
                if (infectionAbs < 0.0f)
                {
                    liquidSetting = ScreenLiquidSettingName.disinfectionStation_Apply;
                }
                ScreenLiquidManager.TryApply(liquidSetting, player.Position, data.ScreenLiquidRange, true);
            }

            player.Damage.ModifyInfection(new pInfection()
            {
                amount = infectionAbs,
                effect = pInfectionEffect.None,
                mode = pInfectionMode.Add
            }, true, true);
        }

        public void OnDead(ushort id)
        {
            _EnemyList.Remove(id);
        }
    }

    public class InfectionAttackData
    {
        public ValueBase Infection = ValueBase.Zero;
        public uint SoundEventID = 0u;
        public bool UseEffect = false;
        public float ScreenLiquidRange = 0.0f;
    }
}
