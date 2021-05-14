using Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EECustom.Customizations.Models.Managers
{
    public class GlowManager : MonoBehaviour
    {
        public bool UsingHibernatePulse = false;
        public bool UsingCombatPulse = false;

        private EnemyAgent _Agent;
        private ES_Hibernate _Hibernate;
        private float _HibernatePulseTimer = 0.0f;
        private float _CombatPulseTimer = 0.0f;
        private float _ScoutPulseTimer = 0.0f;

        private float _UpdateTimerDelay = 0.1f;

        public GlowManager(IntPtr ptr) : base(ptr)
        {
            
        }

        internal void Start()
        {
            _Agent = GetComponentInParent<EnemyAgent>();
            _Hibernate = _Agent.Locomotion.Hibernate;
        }

        internal void Update()
        {
            if (UsingHibernatePulse)
            {
                UpdateHibernatePulse();
            }

            if (UsingCombatPulse)
            {
                UpdateCombatPulse();
            }
        }

        private void UpdateHibernatePulse()
        {
            if (_Agent.AI.Mode != Agents.AgentMode.Agressive)
                return;

            if (_CombatPulseTimer < Clock.Time)
                return;

            _HibernatePulseTimer = Clock.Time + _UpdateTimerDelay;
        }

        private void UpdateCombatPulse()
        {
            if (_Agent.AI.Mode != Agents.AgentMode.Agressive)
                return;

            if (_CombatPulseTimer < Clock.Time)
                return;

            _CombatPulseTimer += Clock.Time + _UpdateTimerDelay;
        }
    }
}
