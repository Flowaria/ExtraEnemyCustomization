﻿using Enemies;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtraEnemyCustomization.Customizations
{
    public class MaterialCustom : EnemyCustomBase
    {
        private static Dictionary<string, Material> _MatDict = new Dictionary<string, Material>();

        public static void AddToCache(string matName, Material mat)
        {
            if (!_MatDict.ContainsKey(matName))
                _MatDict.Add(matName, mat);
        }

        public MaterialSwapSet[] MaterialSets = new MaterialSwapSet[0];

        public override string GetProcessName()
        {
            return "Material";
        }

        public override bool HasPrespawnBody => true;

        public override void Prespawn(EnemyAgent agent)
        {
            var charMats = agent.GetComponentInChildren<CharacterMaterialHandler>().m_materialRefs;
            foreach (var mat in charMats)
            {
                var matName = mat.m_material.name;
                LogDev($" - Debug Info, Material Found: {matName}");

                var swapSet = MaterialSets.SingleOrDefault(x => x.From.Equals(matName));
                if (swapSet == null)
                    continue;

                if (!_MatDict.TryGetValue(swapSet.To, out var newMat))
                {
                    Logger.Error($"MATERIAL IS NOT CACHED!: {swapSet.To}");
                    continue;
                }

                LogDev($" - Trying to Replace Material, Before: {matName} After: {newMat.name}");
                mat.m_material = newMat;
                LogDev(" - Replaced!");
            }
        }
    }

    public class MaterialSwapSet
    {
        public string From = "";
        public string To = "";
    }
}