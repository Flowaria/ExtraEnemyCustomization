using HarmonyLib;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Events.Inject
{
    [HarmonyPatch(typeof(Builder))]
    class Inject_Builder
    {
        [HarmonyPrefix]
        [HarmonyWrapSafe]
        [HarmonyPatch(nameof(Builder.Build))]
        static void Pre_BuildStart()
        {
            LevelEvents.OnBuildStart?.Invoke();
        }

        [HarmonyPostfix]
        [HarmonyWrapSafe]
        [HarmonyPatch(nameof(Builder.OnFactoryDone))]
        static void Post_BuildDone()
        {
            LevelEvents.OnBuildDone?.Invoke();
        }

        [HarmonyPostfix]
        [HarmonyWrapSafe]
        [HarmonyPatch(nameof(Builder.OnLevelCleanup))]
        static void Post_LevelCleanup()
        {
            LevelEvents.OnLevelCleanup?.Invoke();
        }
    }
}
