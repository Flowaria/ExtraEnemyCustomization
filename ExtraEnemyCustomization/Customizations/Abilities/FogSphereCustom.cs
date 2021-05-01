using Enemies;
using UnityEngine;

namespace EECustom.Customizations.Abilities
{
    public class FogSphereCustom : EnemyCustomBase
    {
        public Color ColorMin = Color.white;
        public Color ColorMax = Color.clear;
        public float IntensityMin = 1.0f;
        public float IntensityMax = 5.0f;
        public float RangeMin = 1.0f;
        public float RangeMax = 3.0f;
        public float DensityMin = 1.0f;
        public float DensityMax = 5.0f;
        public float DensityAmountMin = 0.0f;
        public float DensityAmountMax = 5.0f;
        public float Duration = 30.0f;

        public override string GetProcessName()
        {
            return "FogSphere";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
        {
            var eabFog = agent.GetComponentInChildren<EAB_FogSphere>(true);
            var fog = eabFog.m_fogSpherePrefab.GetComponent<FogSphereHandler>();
            if (eabFog != null)
            {
                fog.m_colorMin = ColorMin;
                fog.m_colorMax = ColorMax;
                fog.m_intensityMin = IntensityMin;
                fog.m_intensityMax = IntensityMax;
                fog.m_rangeMin = RangeMin;
                fog.m_rangeMax = RangeMax;
                fog.m_densityMin = DensityMin;
                fog.m_densityMax = DensityMax;
                fog.m_densityAmountMin = DensityAmountMin;
                fog.m_densityAmountMax = DensityAmountMax;
                fog.m_totalLength = Duration;
            }
        }
    }
}