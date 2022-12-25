using Character.Interfaces;
using Common;
using Crops;
using Crops.Interfaces;
using Factories.Interfaces;
using static Common.Fsm<Tiles.Tile>;

namespace Tiles.States
{
    public class TileGrowth: AState<CropType>, ISignalHandler<string>
    {
        private readonly ICropFactory _cropFactory;
        private readonly ICharacter _character;
        private readonly ITile _currentTile;
        private readonly TileCanvas _tileCanvas;

        private CropType _cropType;
        private ICrop _currentCrop;
        
        public TileGrowth(ICropFactory cropFactory, ICharacter character, ITile currentTile, TileCanvas tileCanvas)
        {
            _cropFactory = cropFactory;
            _character = character;
            _currentTile = currentTile;
            _tileCanvas = tileCanvas;
        }
        
        public override void SetStateArg(CropType arg)
        {
            _cropType = arg;
        }
        
        public override void Enter()
        {
            _currentCrop = _cropFactory.Create(_cropType);
            _currentCrop.Initialize(_currentTile);
            _currentCrop.Plant();
            _tileCanvas.StartTimer(_currentCrop.RipeningTime);
            _tileCanvas.OnTimerIsReady += TileIsReady;
        }

        public override void Exit()
        {
            _tileCanvas.OnTimerIsReady -= TileIsReady;
        }

        private void TileIsReady()
        {
            Fsm.ChangeState<TileReady,ICrop>(_currentCrop);
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