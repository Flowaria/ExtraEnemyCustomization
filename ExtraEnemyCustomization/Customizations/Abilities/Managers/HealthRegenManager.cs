using Agents;
using Enemies;
using EECustom.Utils;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using EECustom.Events;

namespace EECustom.Customizations.Abilities.Managers
{
    public class HealthRegenManager : MonoBehaviour
    {
        public Dam_EnemyDamageBase DamageBase;

        public HealthRegenData RegenData;

        private float _RegenInitialTimer = 0.0f;
        private float _RegenIntervalTimer = 0.0f;
        private bool _IsRegening = false;
        private bool _IsInitialTimerDone = false;
        private bool _AlwaysRegen = false;
        private bool _IsDecay = false;
        private float _RegenCapAbsValue = 0.0f;
        private float _RegenAmountAbsValue = 0.0f;

        private Action<EnemyAgent, Agent, float> _OnDamageDel;

        public HealthRegenManager(IntPtr ptr) : base(ptr)
        {
        }

        internal void Start()
        {
            _IsRegening = false;
            _AlwaysRegen = !RegenData.CanDamageInterruptRegen;

            if (!_AlwaysRegen)
            {
                //DamageBase.add_CallOnTakeDamage(new Action<float>(OnTakeDamage)); This doesn't work for some reason rofl
                _OnDamageDel = new Action<EnemyAgent, Agent, float>((EnemyAgent a1, Agent a2, float dmg) => {
                    if (a1.GlobalID == DamageBase.Owner.GlobalID)
                    {
                        OnTakeDamage(dmg);
                    }
                });

                EnemyDamageEvents.OnDamage += _OnDamageDel;
                DamageBase.Owner.add_OnDeadCallback(new Action(() => {
                    EnemyDamageEvents.OnDamage -= _OnDamageDel;
                }));
            }

            _RegenCapAbsValue = RegenData.RegenCap.GetAbsValue(DamageBase.HealthMax);
            _RegenAmountAbsValue = RegenData.RegenAmount.GetAbsValue(DamageBase.HealthMax);

            if (_RegenAmountAbsValue <= 0.0f)
                _IsDecay = true;

            if (_AlwaysRegen || _IsDecay)
            {
                OnTakeDamage(0.0f);
            }
        }

        internal void Update()
        {
            if (!SNet.IsMaster)
                return;

            if (!_IsRegening)
                return;

            if(!_IsInitialTimerDone && _RegenInitialTimer <= Clock.Time)
            {
                _IsInitialTimerDone = true;
            }
            else if (_IsInitialTimerDone && _RegenIntervalTimer <= Clock.Time)
            {
                if (!_IsDecay && DamageBase.Health >= _RegenCapAbsValue)
                    return;

                if (_IsDecay && DamageBase.Health <= _RegenCapAbsValue)
                    return;

                var newHealth = DamageBase.Health + _RegenAmountAbsValue;
                if(!_IsDecay && newHealth >= _RegenCapAbsValue)
                {
                    newHealth = _RegenCapAbsValue;
                    if(!_AlwaysRegen)
                    {
                        _IsRegening = false;
                    }
                }
                else if(_IsDecay && newHealth <= _RegenCapAbsValue)
                {
                    newHealth = _RegenCapAbsValue;
                    if (!_AlwaysRegen)
                    {
                        _IsRegening = false;
                    }
                }

                DamageBase.SendSetHealth(newHealth);

                if (newHealth <= 0.0f)
                {
                    DamageBase.MeleeDamage(DamageBase.HealthMax, null, base.transform.position, Vector3.up, 0);
                }

                _RegenIntervalTimer = Clock.Time + RegenData.RegenInterval;
            }
        }

        void OnTakeDamage(float damage)
        {
            _RegenInitialTimer = Clock.Time + RegenData.DelayUntilRegenStart;
            _IsRegening = true;
            _IsInitialTimerDone = false;
        }
    }
}
