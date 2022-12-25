using Common;
using Inputs.Interfaces;
using UnityEngine;
using Utils;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterPlanting: AState<IFsm>
    {
        private readonly CharacterView _characterView;
        private readonly CharacterModel _characterModel;
        private readonly IMouseService _mouseService;
        private readonly CameraFollow _cameraFollow;
        
        private IFsm _plantingFsm;
        
        private readonly int _plantAnimationId = Animator.StringToHash("Plant");

        public CharacterPlanting(CharacterView characterView, CharacterModel characterModel, 
            IMouseService mouseService, CameraFollow cameraFollow)
        {
            _characterView = characterView;
            _characterModel = characterModel;
            _mouseService = mouseService;
            _cameraFollow = cameraFollow;
        }
        public override void SetStateArg(IFsm arg)
        {
            _plantingFsm = arg;
        }
        
        public override void Enter()
        {
            _characterView.characterAnimator.SetBool(_plantAnimationId, true);
            _mouseService.OnRightClick += PlantingCanceled;
            _characterView.OnPlantAction += PlantingSuccess;
        }

        public override void Exit()
        {
            _cameraFollow.State = _characterModel.characterCameraState;
            _characterView.characterAnimator.SetBool(_plantAnimationId, false);
            _mouseService.OnRightClick -= PlantingCanceled;
            _characterView.OnPlantAction -= PlantingSuccess;
        }

        private void PlantingSuccess()
        {
            Fsm.ChangeState<CharacterStay>();
            _plantingFsm.Signal(PlantingState.Success);
        }

        private void PlantingCanceled(IMouseRightClickable clickable)
        {
            _plantingFsm.Signal(PlantingState.Canceled);
        }

    }
}