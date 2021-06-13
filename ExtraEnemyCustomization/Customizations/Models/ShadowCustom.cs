using Enemies;
using UnityEngine;

namespace EECustom.Customizations.Models
{
    public class ShadowCustom : EnemyCustomBase, IEnemySpawnedEvent
    {
        public bool IncludeEggSack { get; set; } = false;
        public bool RequireTagForDetection { get; set; } = true;

        public override string GetProcessName()
        {
            return "Shadow";
        }

        public void OnSpawned(EnemyAgent agent)
        {
            agent.RequireTagForDetection = RequireTagForDetection;
            agent.MovingCuller.m_disableAnimatorCullingWhenRenderingShadow = true;

            agent.MovingCuller.Culler.Renderers.Clear();
            agent.MovingCuller.Culler.hasShadowsEnabled = true;

            var comps = agent.GetComponentsInChildren<Renderer>(true);
            foreach (var comp in comps)
            {
                if (!IncludeEggSack && comp.gameObject.name.Contains("Egg"))
                {
                    LogVerbose(" - Ignored EggSack Object!");
                    comp.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    comp.enabled = true;
                    continue;
                }

                comp.castShadows = true;
                comp.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                comp.enabled = true;

                var skinmeshrenderer = comp.TryCast<SkinnedMeshRenderer>();
                if (skinmeshrenderer != null)
                {
                    skinmeshrenderer.updateWhenOffscreen = true;
                }
            }
        }
    }
}