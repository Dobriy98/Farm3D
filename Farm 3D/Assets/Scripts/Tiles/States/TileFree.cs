using Character;
using Character.Interfaces;
using Common;
using Crops;
using static Common.Fsm;

namespace Tiles.States
{
    public class TileFree: AState
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
        
        public void PlantingSignal(PlantingState signal)
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
    }
}