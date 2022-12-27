using Character.Interfaces;
using Character.States;
using Common;
using Core;
using Inputs.Interfaces;
using Tiles;
using UnityEngine;
using Utils;

namespace Character
{
    public class MainCharacter : ICharacter, IUpdateListener
    {
        public CharacterView CharacterView { get; set; }
        public CharacterModel CharacterModel { get; set; }

        private readonly Updater _updater;
        private readonly IMouseService _mouseService;
        private readonly CameraFollow _cameraFollow;
        
        private Fsm _fsm;

        public MainCharacter(CharacterView characterView, CharacterModel characterModel, Updater updater,
            IMouseService mouseService, CameraFollow cameraFollow)
        {
            CharacterView = characterView;
            CharacterModel = characterModel;

            _updater = updater;
            _mouseService = mouseService;
            _cameraFollow = cameraFollow;
        }

        public void Initialize()
        {
            _fsm = new Fsm();
            InitializeStates();
            
            _updater.AddListener(this);
            
            CharacterView.OnDestroyHandler += Destroy;
            
        }

        public void MoveTo(Vector3 point)
        {
            CharacterMoveArgs args = new CharacterMoveArgs()
            {
                PointToMove = point,
                AfterMovingState = _fsm.TakeState<CharacterStay>()
            };
            _fsm.ChangeState<CharacterMove, CharacterMoveArgs>(args);
        }

        public void Plant(IPlaceToPlant plantingPlace, Vector3 pointToPlant)
        {
            CharacterMoveArgs args = new CharacterMoveArgs()
            {
                PointToMove = pointToPlant,
                AfterMovingState = _fsm.TakeStateWithArgs<CharacterPlanting, IPlaceToPlant>(plantingPlace)
            };
            _fsm.ChangeState<CharacterMove, CharacterMoveArgs>(args);
        }

        public void Collect(IPlaceToCollect collectingPlace, Vector3 pointToCollect)
        {
            CharacterMoveArgs args = new CharacterMoveArgs()
            {
                PointToMove = pointToCollect,
                AfterMovingState = _fsm.TakeStateWithArgs<CharacterTakeCrop, IPlaceToCollect>(collectingPlace)
            };
            _fsm.ChangeState<CharacterMove, CharacterMoveArgs>(args);
        }

        public void Tick(float deltaTime)
        {
            _fsm.DoUpdate();
        }

        private void Destroy()
        {
            _updater.RemoveListener(this);
            CharacterView.OnDestroyHandler -= Destroy;
        }

        private void InitializeStates()
        {
            CharacterInit characterInit = new CharacterInit(CharacterView, CharacterModel);
            CharacterMove characterMove = new CharacterMove(CharacterView);
            CharacterPlanting characterPlanting = new CharacterPlanting(CharacterView, CharacterModel,
                _mouseService, _cameraFollow);
            CharacterTakeCrop characterTakeCrop = new CharacterTakeCrop(CharacterView, _mouseService);
            CharacterStay characterStay = new CharacterStay(CharacterView);
            
            _fsm.AddState<CharacterInit>(characterInit);
            _fsm.AddState<CharacterMove>(characterMove);
            _fsm.AddState<CharacterPlanting>(characterPlanting);
            _fsm.AddState<CharacterTakeCrop>(characterTakeCrop);
            _fsm.AddState<CharacterStay>(characterStay);
            
            _fsm.ChangeState<CharacterInit>();
        }
    }
}