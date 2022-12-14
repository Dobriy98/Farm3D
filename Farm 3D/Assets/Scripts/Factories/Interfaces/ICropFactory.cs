using Crops;

namespace Factories.Interfaces
{
    public interface ICropFactory
    {
        public Crop Create(CropType cropType);
    }
}