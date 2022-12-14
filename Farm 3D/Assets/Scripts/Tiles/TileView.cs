using System;
using Inputs;
using Inputs.Interfaces;
using UnityEngine;

namespace Tiles
{
    public class TileView : MonoBehaviour, IMouseRightClickable, IMouseLeftClickable
    {
        public event Action OnRightClick;
        public event Action OnLeftClick;
        public event Action OnLeftClickStopInteract;

        public event Action OnDestroyHandler;
        
        public RaycastHit Hit { get; set; }
        public void LeftClickInteract()
        {
            OnLeftClick?.Invoke();
        }

        public void RightClickInteract()
        {
            OnRightClick?.Invoke();
        }
        
        public void LeftClickStopInteract()
        {
            OnLeftClickStopInteract?.Invoke();
        }

        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }
    }
}