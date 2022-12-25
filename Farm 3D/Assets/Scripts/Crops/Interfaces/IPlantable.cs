namespace Crops.Interfaces
{
    public interface IPlantable
    {
        public void Plant();
        public float RipeningTime { get; }
    }
}