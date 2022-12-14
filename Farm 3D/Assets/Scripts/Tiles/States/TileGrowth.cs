using Common;
using Crops;
using static Common.Fsm<Tiles.Tile>;

namespace Tiles.States
{
    public class TileGrowth: AState, ISignalHandler<string>
    {
        private readonly CropType _cropType;

        public TileGrowth(CropType cropType)
        {
            _cropType = cropType;
        }
        public override void Enter()
        {
            Context.CurrentCrop = Context.CropFactory.Create(_cropType);
            Context.CurrentCrop.Initialize(Context);
            Context.CurrentCrop.Plant();
            Context.TileCanvas.StartTimer(Context.CurrentCrop.RipeningTime);
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
    }
}