using Enemies;
using ExtraEnemyCustomization.Customizations;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace ExtraEnemyCustomization.Inject
{
    [HarmonyPatch(typeof(EnemyAgent), nameof(EnemyAgent.Setup))]
    static class Inject_EnemyAgent_Setup
    {
        static void Postfix(EnemyAgent __instance)
        {
            ConfigContext.Current.Customize_Postspawn(__instance);
        }
    }
}
