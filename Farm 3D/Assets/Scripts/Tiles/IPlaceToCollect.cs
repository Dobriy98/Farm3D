using Character;

namespace Tiles
{
    public interface IPlaceToCollect
    {
        public void CollectFromPlace(CollectingState collectingState);
    }
}