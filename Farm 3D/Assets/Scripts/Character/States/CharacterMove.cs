using UnityEngine;
using Utils;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterMove: AState
    {
        private readonly Vector3 _pointToMove;
        private readonly AState _stateAfter;
        
        private readonly int _moveAnimationId = Animator.StringToHash("Move");
        private const float MinDistance = 0;

        public CharacterMove(Vector3 point, AState stateAfter)
        {
            _pointToMove = point;
            _stateAfter = stateAfter;
        }
        public override void Enter()
        {
            Context.CharacterView.navMeshAgent.SetDestination(_pointToMove);
        }

        public override void Update()
        {
            Vector3 velocity = Context.CharacterView.navMeshAgent.velocity;
            Context.CharacterView.characterAnimator.SetFloat(_moveAnimationId, velocity.magnitude);
            
            float distance = Helpers.VectorXZDistance(Context.CharacterView.transform.position, _pointToMove);
  
            if (distance <= MinDistance)
            {
                Fsm.ChangeState(_stateAfter);
            }
        }
    }
}