using Common;
using UnityEngine;

namespace Character.Interfaces
{
    public interface ICollect
    {
        public void Collect(IFsm collectingFsm, Vector3 pointToCollect);
    }
}