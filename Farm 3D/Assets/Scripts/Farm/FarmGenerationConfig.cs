using UnityEngine;

namespace Farm
{
    [CreateAssetMenu]
    public class FarmGenerationConfig : ScriptableObject
    {
        public int rows = 4;
        public int columns = 4;
    }
}