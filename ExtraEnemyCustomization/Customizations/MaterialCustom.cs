using Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExtraEnemyCustomization.Customizations
{
    public class MaterialCustom : EnemyCustomBase
    {
        private static Dictionary<string, Material> _MatDict = new Dictionary<string, Material>();

        public static void AddToCache(string matName, Material mat)
        {
            if(!_MatDict.ContainsKey(matName))
                _MatDict.Add(matName, mat);
        }

        public MaterialSwapSet[] MaterialSets = new MaterialSwapSet[0];

        public override void PreSpawn(EnemyAgent agent)
        {
            Logger.DevMessage($"[MaterialCustom] Trying to Replace Material of {agent.name}");

            var charMats = agent.GetComponentInChildren<CharacterMaterialHandler>().m_materialRefs;
            foreach (var mat in charMats)
            {
                var matName = mat.m_material.name;
                Logger.DevMessage($"[MaterialCustom] - Debug Info, Material Found: {matName}");

                var swapSet = MaterialSets.SingleOrDefault(x=>x.From.Equals(matName));
                if (swapSet == null)
                    continue;

                if (!_MatDict.TryGetValue(swapSet.To, out var newMat))
                {
                    Logger.Error($"MATERIAL IS NOT CACHED!: {swapSet.To}");
                    continue;
                }

                Logger.DevMessage($"[MaterialCustom] - Trying to Replace Material, Before: {matName} After: {newMat.name}");
                mat.m_material = newMat;
                Logger.DevMessage("[MaterialCustom] - Replaced!");
            }

            Logger.DevMessage("[MaterialCustom] Finished Searching!");
        }
    }

    public class MaterialSwapSet
    {
        public string From = "";
        public string To = "";
    }
}
