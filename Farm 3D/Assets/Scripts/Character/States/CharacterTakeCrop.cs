using Common;
using Inputs.Interfaces;
using UnityEngine;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterTakeCrop: AState<IFsm>
    {
        private readonly CharacterView _characterView;
        private readonly IMouseService _mouseService;
        
        private IFsm _collectFsm;
        
        private readonly int _collectAnimationId = Animator.StringToHash("Collect");

        public CharacterTakeCrop(CharacterView characterView, IMouseService mouseService)
        {
            _characterView = characterView;
            _mouseService = mouseService;
        }
        
        public override void SetStateArg(IFsm arg)
        {
            _collectFsm = arg;
        }
        
        public override void Enter()
        {
            _characterView.OnCollectAction += CollectingSuccess;
            _mouseService.OnRightClick += CollectingCanceled;
            _characterView.characterAnimator.SetBool(_collectAnimationId, true);
        }

        public override void Exit()
        {
            _characterView.OnCollectAction -= CollectingSuccess;
            _mouseService.OnRightClick -= CollectingCanceled;
            _characterView.characterAnimator.SetBool(_collectAnimationId, false);
        }

        private void CollectingSuccess()
        {
            Fsm.ChangeState<CharacterStay>();
            _collectFsm.Signal(CollectingState.Success);
        }
        private void CollectingCanceled(IMouseRightClickable clickable)
        {
            _collectFsm.Signal(CollectingState.Canceled);
        }

    }
}