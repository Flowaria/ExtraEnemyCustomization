using HarmonyLib;
using LevelGeneration;

namespace EECustom.Events.Inject
{
    [HarmonyPatch(typeof(Builder))]
    internal class Inject_Builder
    {
        [HarmonyPrefix]
        [HarmonyWrapSafe]
        [HarmonyPatch(nameof(Builder.Build))]
        private static void Pre_BuildStart()
        {
            LevelEvents.OnBuildStart?.Invoke();
        }

        [HarmonyPostfix]
        [HarmonyWrapSafe]
        [HarmonyPatch(nameof(Builder.OnFactoryDone))]
        private static void Post_BuildDone()
        {
            LevelEvents.OnBuildDone?.Invoke();
        }

        [HarmonyPostfix]
        [HarmonyWrapSafe]
        [HarmonyPatch(nameof(Builder.OnLevelCleanup))]
        private static void Post_LevelCleanup()
        {
            LevelEvents.OnLevelCleanup?.Invoke();
        }
    }
}