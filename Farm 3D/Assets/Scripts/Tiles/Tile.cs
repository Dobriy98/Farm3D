using System;
using Character.Interfaces;
using Common;
using Crops;
using Factories.Interfaces;
using Tiles.States;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Tiles
{
    public class Tile : ITile
    {
        public TileView TileView { get; }
        public TileModel TileModel { get; set; }

        public CropType CurrentCropType;

        private TileCanvas _tileCanvas;
        private Fsm<Tile> _fsm;

        private readonly ICropFactory _cropFactory;
        private readonly ICharacter _character;
        private readonly CameraFollow _cameraFollow;
        
        private const string RightClickSignal = "RightClick";

        public Tile(TileView tileView, TileModel tileModel, ICharacter character, ICropFactory cropFactory, CameraFollow cameraFollow)
        {
            TileView = tileView;
            TileModel = tileModel;
            
            _character = character;
            _cropFactory = cropFactory;
            _cameraFollow = cameraFollow;
        }
        
        public void Initialize()
        {
            CreateTileCanvas();
            
            TileView.OnLeftClick += LeftClickInteract;
            TileView.OnRightClick += RightClickInteract;
            TileView.OnLeftClickStopInteract += OnLeftClickStopInteract;
            TileView.OnDestroyHandler += Destroy;
            
            _fsm = new Fsm<Tile>();
            InitializeStates();
        }
        
        private void CreateTileCanvas()
        {
            _tileCanvas = Object.Instantiate(TileModel.tileUIConfig.tileCanvasPrefab, TileView.transform);
            _tileCanvas.Initialize(this, TileModel);
        }

        private void RightClickInteract()
        {
            _fsm.Signal(RightClickSignal);
        }

        private void LeftClickInteract()
        {
            _tileCanvas.ShowCanvas(true);
        }

        private void OnLeftClickStopInteract()
        {
            _tileCanvas.ShowCanvas(false);
        }

        public void Plant(CropType cropType)
        {
            CurrentCropType = cropType;
            Vector3 toPoint = Helpers.PointBetween(TileView.transform.position,
                _character.CharacterView.transform.position, 0.5f);
            _character.Plant(_fsm, toPoint);
            
            TileModel.tileCameraState.target = TileView.transform;
            _cameraFollow.State = TileModel.tileCameraState;
            
            _tileCanvas.ShowButtons(false);
        }

        private void Destroy()
        {
            TileView.OnLeftClick -= LeftClickInteract;
            TileView.OnRightClick -= RightClickInteract;
            TileView.OnLeftClickStopInteract -= OnLeftClickStopInteract;
            TileView.OnDestroyHandler -= Destroy;
        }

        private void InitializeStates()
        {
            TileGrowth tileGrowthState = new TileGrowth(_cropFactory, _character, this, _tileCanvas);
            TileFree tileFreeState = new TileFree(_tileCanvas, _character, this);
            TileReady tileReadyState = new TileReady(_character, _tileCanvas, this);

            _fsm.AddState<TileGrowth>(tileGrowthState);
            _fsm.AddState<TileFree>(tileFreeState);
            _fsm.AddState<TileReady>(tileReadyState);
            
            _fsm.ChangeState<TileFree>();
        }
    }
}