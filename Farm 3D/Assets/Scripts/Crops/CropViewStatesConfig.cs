using System;
using UnityEngine;

namespace Crops
{
    [Serializable]
    public class CropViewStatesConfig
    {
        [Range(0,100)] public float percentToSetView;
        public CropView cropViewStatePrefab;
    }
}