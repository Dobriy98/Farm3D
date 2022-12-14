using UI;
using UnityEngine;

namespace Utils
{
    public static class Helpers
    {
        public static float VectorXZDistance (Vector3 v1, Vector3 v2)
        {
            float xDiff = v1.x - v2.x;
            float zDiff = v1.z - v2.z;
            return Mathf.Sqrt((xDiff * xDiff) + (zDiff * zDiff));
        }
        
        public static Vector3 PointBetween(Vector3 v1, Vector3 v2, float distance)
        {
            float Rab = Mathf.Sqrt((v2.x - v1.x) * (v2.x - v1.x) + (v2.z - v1.z) * (v2.z - v1.z));
            float k = distance/Rab;
            float v3X = v1.x + (v2.x - v1.x) * k;
            float v3Z = v1.z + (v2.z - v1.z) * k;
            return new Vector3(v3X, 0, v3Z);
        }
        
        public static ObservableUI<T> FindObservableUI<T>(this GameObject ctx, string key)
        {
            var model = ctx.GetComponentInParent<CanvasModel>();
            if (model == null) return null;
            
            var info = model.GetType().GetField(key);
            if (info == null) return null;
            
            return info.GetValue(model) as ObservableUI<T>;
        }
    }
}