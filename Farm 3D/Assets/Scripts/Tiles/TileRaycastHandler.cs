using Inputs;
using Inputs.Interfaces;

namespace Tiles
{
    public class TileRaycastHandler
    {
        private readonly IMouseService _mouseService;

        private TileView _currentLeftClickableTile;
        
        public TileRaycastHandler(IMouseService mouseService)
        {
            _mouseService = mouseService;

            _mouseService.OnLeftClick += LeftClick;
            _mouseService.OnRightClick += RightClick;
            _mouseService.OnHitNothing += HitNothing;
        }

        ~TileRaycastHandler()
        {
            _mouseService.OnLeftClick -= LeftClick;
            _mouseService.OnRightClick -= RightClick;
            _mouseService.OnHitNothing -= HitNothing;
        }

        private void LeftClick(IMouseLeftClickable leftClickableTile)
        {
            if (_currentLeftClickableTile != null)
            {
                _currentLeftClickableTile.LeftClickStopInteract();
            }
            leftClickableTile.LeftClickInteract();
            _currentLeftClickableTile = leftClickableTile as TileView;
        }
        
        private void RightClick(IMouseRightClickable rightClickableTile)
        {
            rightClickableTile.RightClickInteract();
        }

        private void HitNothing()
        {
            if(_currentLeftClickableTile != null) _currentLeftClickableTile.LeftClickStopInteract();
            _currentLeftClickableTile = null;
        }
    }
}