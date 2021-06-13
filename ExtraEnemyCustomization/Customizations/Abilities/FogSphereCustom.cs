using Enemies;
using UnityEngine;

namespace EECustom.Customizations.Abilities
{
    public class FogSphereCustom : EnemyCustomBase, IEnemySpawnedEvent
    {
        public Color ColorMin { get; set; } = Color.white;
        public Color ColorMax { get; set; } = Color.clear;
        public float IntensityMin { get; set; } = 1.0f;
        public float IntensityMax { get; set; } = 5.0f;
        public float RangeMin { get; set; } = 1.0f;
        public float RangeMax { get; set; } = 3.0f;
        public float DensityMin { get; set; } = 1.0f;
        public float DensityMax { get; set; } = 5.0f;
        public float DensityAmountMin { get; set; } = 0.0f;
        public float DensityAmountMax { get; set; } = 5.0f;
        public float Duration { get; set; } = 30.0f;

        public override string GetProcessName()
        {
            return "FogSphere";
        }

        public void OnSpawned(EnemyAgent agent)
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