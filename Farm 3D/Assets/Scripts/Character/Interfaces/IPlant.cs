using Tiles;
using UnityEngine;

namespace Character.Interfaces
{
    public interface IPlant
    {
        public void Plant(IPlaceToPlant plantingPlace, Vector3 pointToPlant);
    }
}