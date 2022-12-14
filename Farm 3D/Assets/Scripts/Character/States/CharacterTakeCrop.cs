using Common;
using Inputs.Interfaces;
using UnityEngine;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterTakeCrop: AState
    {
        private readonly IFsm _collectFsm;
        
        private readonly int _collectAnimationId = Animator.StringToHash("Collect");

        public CharacterTakeCrop(IFsm collectFsm)
        {
            _collectFsm = collectFsm;
        }
        public override void Enter()
        {
            Context.CharacterView.OnCollectAction += CollectingSuccess;
            Context.MouseService.OnRightClick += CollectingCanceled;
            Context.CharacterView.characterAnimator.SetBool(_collectAnimationId, true);
        }

        public override void Exit()
        {
            Context.CharacterView.OnCollectAction -= CollectingSuccess;
            Context.MouseService.OnRightClick -= CollectingCanceled;
            Context.CharacterView.characterAnimator.SetBool(_collectAnimationId, false);
        }

        private void CollectingSuccess()
        {
            Fsm.ChangeState(new CharacterStay());
            _collectFsm.Signal(CollectingState.Success);
        }
        private void CollectingCanceled(IMouseRightClickable clickable)
        {
            _collectFsm.Signal(CollectingState.Canceled);
        }
    }
}