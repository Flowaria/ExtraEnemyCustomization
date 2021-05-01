using Enemies;
using UnityEngine;

namespace EECustom.Customizations.Models
{
    public class ShadowCustom : EnemyCustomBase
    {
        public bool IncludeEggSack = false;
        public bool RequireTagForDetection = true;

        public override string GetProcessName()
        {
            return "Shadow";
        }

        public override bool HasPostspawnBody => true;

        public override void Postspawn(EnemyAgent agent)
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
                    LogDev(" - Ignored EggSack Object!");
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