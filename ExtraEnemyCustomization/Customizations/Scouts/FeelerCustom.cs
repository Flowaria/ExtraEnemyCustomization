using EECustom.Utils;
using Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EECustom.Customizations.Scouts
{
    public class FeelerCustom : EnemyCustomBase
    {
        public ValueBase TendrilCount = ValueBase.Unchanged;
        public float TendrilAngleOffset = 0.0f;
        public ValueBase TendrilStepAngle = ValueBase.Unchanged;
        public ValueBase TendrilMinYSpread = ValueBase.Unchanged;
        public ValueBase TendrilMaxYSpread = ValueBase.Unchanged;

        public ValueBase Distance = ValueBase.Unchanged;
        public ValueBase StepDistance = ValueBase.Unchanged;
        public ValueBase FoldTime = ValueBase.Unchanged;
        public ValueBase RetractTime = ValueBase.Unchanged;
        public ValueBase RetractTimeDetected = ValueBase.Unchanged;
        public Color NormalColor = Color.white;
        public Color DetectColor = Color.red;

        //TODO: Implement me Daddy
        public override string GetProcessName()
        {
            return "ScoutFeeler";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            var onDetectionSpawn = new Action<EnemyAgent, ScoutAntennaDetection>((EnemyAgent eventAgent, ScoutAntennaDetection detection) =>
            {
                if(eventAgent.GlobalID == agent.GlobalID)
                    OnDetectionSpawn(eventAgent, detection);
            });

            var onAntennaSpawn = new Action<EnemyAgent, ScoutAntennaDetection, ScoutAntenna>((EnemyAgent eventAgent, ScoutAntennaDetection detection, ScoutAntenna ant) =>
            {
                if (eventAgent.GlobalID == agent.GlobalID)
                    OnAntennaSpawn(eventAgent, detection, ant);
            });

            ScoutAntennaSpawnEvent.OnDetectionSpawn += onDetectionSpawn;
            ScoutAntennaSpawnEvent.OnAntennaSpawn += onAntennaSpawn;
            agent.add_OnDeadCallback(new Action(()=>
            {
                ScoutAntennaSpawnEvent.OnDetectionSpawn -= onDetectionSpawn;
                ScoutAntennaSpawnEvent.OnAntennaSpawn -= onAntennaSpawn;
            }));
        }

        private void OnDetectionSpawn(EnemyAgent agent, ScoutAntennaDetection detection)
        {
            detection.m_tendrilCount = TendrilCount.GetAbsValue(detection.m_tendrilCount);
            detection.m_dirAngOffset = TendrilAngleOffset;
            detection.m_dirAngStep = TendrilStepAngle.GetAbsValue(detection.m_dirAngStep);
            detection.m_dirAngSpread_Min = TendrilMinYSpread.GetAbsValue(detection.m_dirAngSpread_Min);
            detection.m_dirAngSpread_Max = TendrilMaxYSpread.GetAbsValue(detection.m_dirAngSpread_Max);
        }

        private void OnAntennaSpawn(EnemyAgent agent, ScoutAntennaDetection detection, ScoutAntenna ant)
        {
            ant.m_colorDefault = NormalColor;
            ant.m_colorDetection = DetectColor;
            ant.m_moveOutTime = FoldTime.GetAbsValue(ant.m_moveOutTime);
            ant.m_moveInTime = RetractTime.GetAbsValue(ant.m_moveInTime);
            ant.m_moveInTimeDetected = RetractTimeDetected.GetAbsValue(ant.m_moveInTimeDetected);
            ant.m_maxDistance = Distance.GetAbsValue(ant.m_maxDistance);
            ant.m_stepDistance = StepDistance.GetAbsValue(ant.m_stepDistance);
        }
    }
}
