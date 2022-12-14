using Character.Interfaces;
using Character.States;
using Common;
using Core;
using Inputs.Interfaces;
using UnityEngine;
using Utils;

namespace Character
{
    public class MainCharacter : ICharacter, IUpdateListener
    {
        public CharacterView CharacterView { get; set; }
        public CharacterModel CharacterModel { get; set; }

        public readonly Updater Updater;
        public readonly IMouseService MouseService;
        public readonly CameraFollow CameraFollow;
        
        private Fsm<MainCharacter> _fsm;

        public MainCharacter(CharacterView characterView, CharacterModel characterModel, Updater updater,
            IMouseService mouseService, CameraFollow cameraFollow)
        {
            CharacterView = characterView;
            CharacterModel = characterModel;

            Updater = updater;
            MouseService = mouseService;
            CameraFollow = cameraFollow;
        }

        public void Initialize()
        {
            _fsm = new Fsm<MainCharacter>(this, new CharacterInit());
        }

        public void MoveTo(Vector3 point)
        {
            _fsm.ChangeState(new CharacterMove(point, new CharacterStay()));
        }

        public void Plant(IFsm plantingFsm, Vector3 pointToPlant)
        {
            _fsm.ChangeState(new CharacterMove(pointToPlant, new CharacterPlanting(plantingFsm)));
        }

        public void Collect(IFsm collectFsm, Vector3 pointToCollect)
        {
            _fsm.ChangeState(new CharacterMove(pointToCollect, new CharacterTakeCrop(collectFsm)));
        }

        public void Tick(float deltaTime)
        {
            _fsm.DoUpdate();
        }

        public void Destroy()
        {
            Updater.RemoveListener(this);
            CharacterView.OnDestroyHandler -= Destroy;
        }
    }
}