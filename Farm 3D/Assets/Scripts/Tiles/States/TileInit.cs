using UnityEngine;
using static Common.Fsm<Tiles.Tile>;

namespace Tiles.States
{
    public class TileInit: AState
    {
        public override void Enter()
        {
            CreateTileCanvas();
            Context.TileView.OnLeftClick += Context.LeftClickInteract;
            Context.TileView.OnRightClick += Context.RightClickInteract;
            Context.TileView.OnLeftClickStopInteract += Context.OnLeftClickStopInteract;

            Context.TileView.OnDestroyHandler += Context.Destroy;
            Fsm.ChangeState(new TileFree());
        }

        private void CreateTileCanvas()
        {
            Context.TileCanvas = Object.Instantiate(Context.TileModel.tileUIConfig.tileCanvasPrefab, Context.TileView.transform);
            Context.TileCanvas.Initialize(Context, Context.TileModel);
        }
    }
}