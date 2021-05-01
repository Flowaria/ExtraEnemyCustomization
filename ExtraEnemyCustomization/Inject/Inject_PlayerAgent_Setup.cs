using EECustom.Utils;
using HarmonyLib;
using Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECustom.Inject
{
    [HarmonyPatch(typeof(PlayerAgent), nameof(PlayerAgent.Setup))]
    public class Inject_PlayerAgent_Setup
    {
        static void Postfix(PlayerAgent __instance)
        {
            PlayerData.MaxHealth = __instance.PlayerData.health;
        }
    }
}
