using Common;
using Inputs.Interfaces;
using UnityEngine;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterPlanting: AState
    {
        private readonly IFsm _plantingFsm;
        
        private readonly int _plantAnimationId = Animator.StringToHash("Plant");

        public CharacterPlanting(IFsm plantingFsm)
        {
            _plantingFsm = plantingFsm;
        }
        public override void Enter()
        {
            Context.CharacterView.characterAnimator.SetBool(_plantAnimationId, true);
            Context.MouseService.OnRightClick += PlantingCanceled;
            Context.CharacterView.OnPlantAction += PlantingSuccess;
        }

        public override void Exit()
        {
            Context.CameraFollow.State = Context.CharacterModel.characterCameraState;
            Context.CharacterView.characterAnimator.SetBool(_plantAnimationId, false);
            Context.MouseService.OnRightClick -= PlantingCanceled;
            Context.CharacterView.OnPlantAction -= PlantingSuccess;
        }

        private void PlantingSuccess()
        {
            Fsm.ChangeState(new CharacterStay());
            _plantingFsm.Signal(PlantingState.Success);
        }

        private void PlantingCanceled(IMouseRightClickable clickable)
        {
            _plantingFsm.Signal(PlantingState.Canceled);
        }
    }
}