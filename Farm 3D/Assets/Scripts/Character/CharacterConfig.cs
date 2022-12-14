using UnityEngine;

namespace Character
{
    [CreateAssetMenu]
    public class CharacterConfig : ScriptableObject
    {
        public CharacterView characterPrefab;
        public CharacterModel characterModel;
    }
}