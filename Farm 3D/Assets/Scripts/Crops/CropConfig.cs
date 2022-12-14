using UnityEngine;

namespace Crops
{
    [CreateAssetMenu]
    public class CropConfig: ScriptableObject
    {
        public CropModel cropModel;
        public CropUI cropUI;
        public CropViewStatesConfig[] cropViewStatesConfig;
    }
}