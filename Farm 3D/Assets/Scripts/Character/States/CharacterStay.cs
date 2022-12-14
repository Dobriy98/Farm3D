using UnityEngine;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterStay: AState
    {
        private readonly int _moveAnimationId = Animator.StringToHash("Move");

        public override void Enter()
        {
            Context.CharacterView.navMeshAgent.velocity = Vector3.zero;
            Context.CharacterView.characterAnimator.SetFloat(_moveAnimationId, 0);
        }
    }
}