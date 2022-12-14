using System;

namespace Crops
{
    [Serializable]
    public class CropModel
    {
        public CropType cropType;
        public float ripeningTime;
        public bool isCollectable = true;
        public int pointsToAdd = 1;
    }
}