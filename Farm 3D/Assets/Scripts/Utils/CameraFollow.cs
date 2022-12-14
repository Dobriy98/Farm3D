using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class CameraState
    {
        public Transform target;
        public Vector3 positionOffset = Vector3.up;
        public Quaternion rotationOffset = Quaternion.identity;
        public float distance = 10;
        public float speed = 1;
    }
    
    public class CameraFollow : MonoBehaviour
    {

        public CameraState State;
        private Vector3 focusPoint;
        private float distance = 10;

        private void LateUpdate()
        {
            if (State?.target == null) return;
            var tm = Time.deltaTime * State.speed;

            transform.rotation = Quaternion.Lerp(transform.rotation,State.rotationOffset,tm);
            focusPoint = Vector3.Lerp(focusPoint, State.target.position + State.positionOffset, tm);
            distance = Mathf.Lerp(distance, State.distance,tm);
            transform.position  = focusPoint - transform.forward * distance;
        }
    }
}
