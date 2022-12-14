using Tiles;

namespace Crops.Interfaces
{
    public interface ICrop : IPlantable, ICollectable
    {
        public CropModel CropModel { get; set; }
        public void Initialize(ITile tile);
    }
}