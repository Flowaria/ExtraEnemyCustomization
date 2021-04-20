using Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ExtraEnemyCustomization.Customizations
{
    public class ShadowCustomization : EnemyCustomizationBase
    {
        public bool IncludeEggSack = false;
        public bool RequireTagForDetection = true;

        public override void PostSpawn(EnemyAgent agent)
        {
            Logger.DevMessage($"[ShadowCustom] Applying Shadow Effect to {agent.name}");

            agent.RequireTagForDetection = RequireTagForDetection;
            agent.MovingCuller.m_disableAnimatorCullingWhenRenderingShadow = true;

            agent.MovingCuller.Culler.Renderers.Clear();
            agent.MovingCuller.Culler.hasShadowsEnabled = true;

            var comps = agent.GetComponentsInChildren<Renderer>(true);
            foreach (var comp in comps)
            {
                if (!IncludeEggSack && comp.gameObject.name.Contains("Egg"))
                {
                    Logger.DevMessage("[ShadowCustom] - Ignored EggSack Object!");
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

            Logger.DevMessage("[ShadowCustom] Applied!");
        }
    }
}
