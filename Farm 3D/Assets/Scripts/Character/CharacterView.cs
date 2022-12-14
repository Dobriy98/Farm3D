using System;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterView : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Animator characterAnimator;

        public event Action OnDestroyHandler;
        public event Action OnPlantAction;
        public event Action OnCollectAction;
        
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            characterAnimator = GetComponent<Animator>();
        }

        public void SetMovementSpeed(float speed)
        {
            navMeshAgent.speed = speed;
        }

        public void SetInitPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void EndPlanting()
        {
            OnPlantAction?.Invoke();
        }

        public void EndCollecting()
        {
            OnCollectAction?.Invoke();
        }
        
        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }
    }
}