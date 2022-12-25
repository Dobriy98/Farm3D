using System;
using Character;
using Character.Interfaces;
using Common;
using Crops;
using static Common.Fsm<Tiles.Tile>;

namespace Tiles.States
{
    public class TileFree: AState, ISignalHandler<PlantingState>, ISignalHandler<string>
    {
        private readonly TileCanvas _tileCanvas;
        private readonly ICharacter _character;
        private readonly Tile _currentTile;
        
        public TileFree(TileCanvas tileCanvas, ICharacter character, Tile currentTile)
        {
            _tileCanvas = tileCanvas;
            _character = character;
            _currentTile = currentTile;
        }
        public void Signal(PlantingState signal)
        {
            switch (signal)
            {
                case PlantingState.Success:
                    Fsm.ChangeState<TileGrowth, CropType>(_currentTile.CurrentCropType);
                    break;
                case PlantingState.Canceled:
                    _tileCanvas.ShowButtons(true);
                    _tileCanvas.ShowCanvas(false);
                    break;
            }
        }
        
        public void Signal(string signal)
        {
            switch (signal)
            {
                case "RightClick":
                    _character.MoveTo(_currentTile.TileView.Hit.point);
                    break;
            }
        }
    }
}