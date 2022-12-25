using Core;
using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterInit: AState
    {
        private readonly CharacterView _characterView;
        private readonly CharacterModel _characterModel;
        public CharacterInit(CharacterView characterView, CharacterModel characterModel)
        {
            _characterView = characterView;
            _characterModel = characterModel;
        }
        public override void Enter()
        {
            _characterModel.characterCameraState.target = _characterView.transform;

            _characterView.SetInitPosition(_characterModel.initPosition);
            _characterView.SetMovementSpeed(_characterModel.walkSpeed);
        }
    }
}