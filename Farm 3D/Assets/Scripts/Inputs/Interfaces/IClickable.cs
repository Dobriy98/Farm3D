using UnityEngine;

namespace Inputs.Interfaces
{
    public interface IClickable
    {
        RaycastHit Hit { get; set; }
    }
}