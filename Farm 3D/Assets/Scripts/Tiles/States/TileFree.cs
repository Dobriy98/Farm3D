using Character;
using Common;
using Crops;
using static Common.Fsm<Tiles.Tile>;

namespace Tiles.States
{
    public class TileFree: AState, ISignalHandler<PlantingState>, ISignalHandler<string>, ISignalHandler<CropType>
    {
        private CropType _currentCropType;
        public void Signal(PlantingState signal)
        {
            switch (signal)
            {
                case PlantingState.Success:
                    Fsm.ChangeState(new TileGrowth(_currentCropType));
                    break;
                case PlantingState.Canceled:
                    Context.TileCanvas.ShowButtons(true);
                    Context.TileCanvas.ShowCanvas(false);
                    break;
            }
        }
        
        public void Signal(string signal)
        {
            switch (signal)
            {
                case "RightClick":
                    Context.Character.MoveTo(Context.TileView.Hit.point);
                    break;
            }
        }

        public void Signal(CropType signal)
        {
            _currentCropType = signal;
        }
    }
}