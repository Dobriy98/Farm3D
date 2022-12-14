using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu]
    public class TileConfig : ScriptableObject
    {
        public TileView tileViewPrefab;
        public TileModel tileModel;
    }
}