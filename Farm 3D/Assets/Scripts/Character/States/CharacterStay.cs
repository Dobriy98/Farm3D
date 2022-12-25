using UnityEngine;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterStay: AState
    {
        private readonly CharacterView _characterView;
        private readonly int _moveAnimationId = Animator.StringToHash("Move");

        public CharacterStay(CharacterView characterView)
        {
            _characterView = characterView;
        }

        public override void Enter()
        {
            _characterView.navMeshAgent.velocity = Vector3.zero;
            _characterView.characterAnimator.SetFloat(_moveAnimationId, 0);
        }
    }
}