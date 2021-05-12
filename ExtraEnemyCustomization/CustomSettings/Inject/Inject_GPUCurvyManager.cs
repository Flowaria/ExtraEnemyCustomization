using EECustom.Managers;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.CustomSettings.Inject
{
    [HarmonyPatch(typeof(GPUCurvyManager))]
    class Inject_GPUCurvyManager
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(GPUCurvyManager.Setup))]
        static void Post_Setup()
        {
            foreach (var tentDef in ConfigManager.Current.TentacleCustom.TentacleDefinitions)
            {
                CustomTentacle.GenerateTentacle(tentDef);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(GPUCurvyManager.TryAllocateGPUCurvy))]
        static bool Pre_Allocate(GPUCurvyType type, ref GPUCurvy gpuCurvy)
        {
            var id = (int)type;
            if (Enum.IsDefined(typeof(GPUCurvyType), id))
            {
                return true;
            }

            var setup = CustomTentacle.GetTentacle(id);
            if (setup.TryCanAllocate(out gpuCurvy))
            {
                Logger.Error($"ALLOC {id}");
                return false;
            }
            Logger.Error($"CANT FIND TENTACLE SETTING WITH ID: {id}");
            return true;
        }
    }
}
