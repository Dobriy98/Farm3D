using Common;
using Tiles;
using UnityEngine;

namespace Character.Interfaces
{
    public interface ICollect
    {
        public void Collect(IPlaceToCollect collectingPlace, Vector3 pointToCollect);
    }
}