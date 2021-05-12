using EECustom.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UnityEngine;

namespace EECustom.CustomSettings.DTO
{
    public class CustomTentacle
    {
        public string DebugName = string.Empty;
        public int ID = 15;
        public GPUCurvyType BodyPrefab = GPUCurvyType.Striker;
        public GPUCurvyType BodyMaterial = GPUCurvyType.Striker;
        public GPUCurvyType HeadPrefab = GPUCurvyType.Striker;
        public GPUCurvyType HeadMaterial = GPUCurvyType.Striker;
        public GPUCurvyType Shape = GPUCurvyType.Striker;
        public int MaxCount = 50;
    }
}
