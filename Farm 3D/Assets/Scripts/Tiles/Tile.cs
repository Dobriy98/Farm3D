using Character.Interfaces;
using Common;
using Crops;
using Crops.Interfaces;
using Factories.Interfaces;
using Tiles.States;
using UnityEngine;
using Utils;

namespace Tiles
{
    public class Tile : ITile
    {
        public TileView TileView { get; set; }
        public TileModel TileModel { get; set; }
        
        public TileCanvas TileCanvas;
        
        public ICrop CurrentCrop;
        
        private Fsm<Tile> _fsm;

        public readonly ICropFactory CropFactory;
        public readonly ICharacter Character;

        private readonly CameraFollow _cameraFollow;
        
        private const string RightClickSignal = "RightClick";

        public Tile(TileView tileView, TileModel tileModel, ICharacter character, ICropFactory cropFactory, CameraFollow cameraFollow)
        {
            TileView = tileView;
            TileModel = tileModel;
            
            Character = character;
            CropFactory = cropFactory;
            _cameraFollow = cameraFollow;
        }
        
        public void Initialize()
        {
            _fsm = new Fsm<Tile>(this, new TileInit());
        }

        public void ReadyToCollect()
        {
            _fsm.ChangeState(new TileReady());
        }

        public void RightClickInteract()
        {
            _fsm.Signal(RightClickSignal);
        }

        public void LeftClickInteract()
        {
            TileCanvas.ShowCanvas(true);
        }

        public void OnLeftClickStopInteract()
        {
            TileCanvas.ShowCanvas(false);
        }

        public void Plant(CropType cropType)
        {
            _fsm.Signal(cropType);

            Vector3 toPoint = Helpers.PointBetween(TileView.transform.position,
                Character.CharacterView.transform.position, 0.5f);
            Character.Plant(_fsm, toPoint);
            
            TileModel.tileCameraState.target = TileView.transform;
            _cameraFollow.State = TileModel.tileCameraState;
            
            TileCanvas.ShowButtons(false);
        }

        public void Destroy()
        {
            TileView.OnLeftClick -= LeftClickInteract;
            TileView.OnRightClick -= RightClickInteract;
            TileView.OnLeftClickStopInteract -= OnLeftClickStopInteract;

            TileView.OnDestroyHandler -= Destroy;
        }
    }
}