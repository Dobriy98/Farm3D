using System;
using UnityEngine;
using Utils;

namespace Character
{
    [Serializable]
    public class CharacterModel
    {
        public Vector3 initPosition;
        public float walkSpeed;
        public CameraState characterCameraState;
    }
}