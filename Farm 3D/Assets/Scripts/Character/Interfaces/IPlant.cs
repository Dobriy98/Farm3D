using Common;
using UnityEngine;

namespace Character.Interfaces
{
    public interface IPlant
    {
        public void Plant(IFsm plantingFsm, Vector3 pointToPlant);
    }
}