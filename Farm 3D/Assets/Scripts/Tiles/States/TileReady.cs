using Character;
using Character.Interfaces;
using Crops.Interfaces;
using UnityEngine;
using Utils;
using static Common.Fsm;

namespace Tiles.States
{
    public class TileReady: AState<ICrop>
    {
        private readonly ICharacter _character;
        private readonly TileCanvas _tileCanvas;
        private readonly ITile _currentTile;
        
        private ICrop _currentCrop;

        public TileReady(ICharacter character, TileCanvas tileCanvas, ITile currentTile)
        {
            _character = character;
            _tileCanvas = tileCanvas;
            _currentTile = currentTile;
        }

        public override void SetStateArg(ICrop arg)
        {
            _currentCrop = arg;
        }

        public void CollectingSignal(CollectingState signal)
        {
            switch (signal)
            {
                case CollectingState.Success:
                    _currentCrop.Collect();
                    _tileCanvas.HideTimer();
                    _tileCanvas.ShowButtons(true);
            
                    Fsm.ChangeState<TileFree>();
                    break;
                case CollectingState.Canceled:
                    break;
            }
            
            _tileCanvas.ShowCanvas(false);
        }

        public void RightClickSignal()
        {
            if (_currentCrop.CropModel.isCollectable)
            {
                Vector3 toPoint = Helpers.PointBetween(_currentTile.TileView.transform.position,
                    _character.CharacterView.transform.position, 0.5f);
                _character.Collect(_currentTile, toPoint);
            }
            else
            {
                _character.MoveTo(_currentTile.TileView.Hit.point);
            }
        }
    }
}