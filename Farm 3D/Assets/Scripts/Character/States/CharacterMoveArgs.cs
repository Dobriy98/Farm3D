using Common;
using UnityEngine;

namespace Character.States
{
    public struct CharacterMoveArgs
    {
        public Vector3 PointToMove;
        public Fsm<MainCharacter>.AState AfterMovingState;
    }
}