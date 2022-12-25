using UnityEngine;
using Utils;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterMove: AState<CharacterMoveArgs>
    {
        private readonly CharacterView _characterView;
        private Vector3 _pointToMove;
        private AState _stateAfterType;
        
        private readonly int _moveAnimationId = Animator.StringToHash("Move");
        private const float MinDistance = 0;

        public CharacterMove(CharacterView characterView)
        {
            _characterView = characterView;
        }
        public override void SetStateArg(CharacterMoveArgs arg)
        {
            _pointToMove = arg.PointToMove;
            _stateAfterType = arg.AfterMovingState;
        }

        public override void Enter()
        {
            _characterView.navMeshAgent.SetDestination(_pointToMove);
        }

        public override void Update()
        {
            Vector3 velocity = _characterView.navMeshAgent.velocity;
            _characterView.characterAnimator.SetFloat(_moveAnimationId, velocity.magnitude);
            
            float distance = Helpers.VectorXZDistance(_characterView.transform.position, _pointToMove);
  
            if (distance <= MinDistance)
            {
                Fsm.ChangeState(_stateAfterType);
            }
        }
    }
}