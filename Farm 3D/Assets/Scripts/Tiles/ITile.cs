namespace Tiles
{
    public interface ITile: IPlaceToPlant, IPlaceToCollect
    {
        public TileView TileView { get; }
        public TileModel TileModel { get; set; }
    }
}