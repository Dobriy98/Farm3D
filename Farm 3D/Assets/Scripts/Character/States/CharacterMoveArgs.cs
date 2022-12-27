using UnityEngine;
using static Common.Fsm;

namespace Character.States
{
    public struct CharacterMoveArgs
    {
        public Vector3 PointToMove;
        public AState AfterMovingState;
    }
}