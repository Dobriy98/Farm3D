using Character;
using Common;
using UnityEngine;
using Utils;
using static Common.Fsm<Tiles.Tile>;

namespace Tiles.States
{
    public class TileReady: AState, ISignalHandler<CollectingState>, ISignalHandler<string>
    {
        public void Signal(CollectingState signal)
        {
            switch (signal)
            {
                case CollectingState.Success:
                    Context.CurrentCrop.Collect();
                    Context.TileCanvas.HideTimer();
                    Context.TileCanvas.ShowButtons(true);
            
                    Fsm.ChangeState(new TileFree());
                    break;
                case CollectingState.Canceled:
                    break;
            }
            
            Context.TileCanvas.ShowCanvas(false);
        }

        public void Signal(string signal)
        {
            switch (signal)
            {
                case "RightClick":
                    if (Context.CurrentCrop.CropModel.isCollectable){
                        Vector3 toPoint = Helpers.PointBetween(Context.TileView.transform.position,
                            Context.Character.CharacterView.transform.position, 0.5f);
                        Context.Character.Collect(Fsm, toPoint);
                    }
                    else
                    {
                        Context.Character.MoveTo(Context.TileView.Hit.point);
                    }
                    break;
            }
        }
    }
}