using Common;
using Inputs.Interfaces;
using Tiles;
using UnityEngine;
using static Common.Fsm;

namespace Character.States
{
    public class CharacterTakeCrop: AState<IPlaceToCollect>
    {
        private readonly CharacterView _characterView;
        private readonly IMouseService _mouseService;
        
        private IPlaceToCollect _collectPlace;
        
        private readonly int _collectAnimationId = Animator.StringToHash("Collect");

        public CharacterTakeCrop(CharacterView characterView, IMouseService mouseService)
        {
            _characterView = characterView;
            _mouseService = mouseService;
        }
        
        public override void SetStateArg(IPlaceToCollect arg)
        {
            _collectPlace = arg;
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
            _collectPlace.CollectFromPlace(CollectingState.Success);
        }
        private void CollectingCanceled(IMouseRightClickable clickable)
        {
            _collectPlace.CollectFromPlace(CollectingState.Canceled);
        }

    }
}