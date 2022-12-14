using System;

namespace Inputs.Interfaces
{
    public interface IMouseService
    {
        event Action<IMouseRightClickable> OnRightClick;
        event Action<IMouseLeftClickable> OnLeftClick;
        event Action OnHitNothing;
    }
}