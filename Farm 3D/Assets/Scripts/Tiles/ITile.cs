namespace Tiles
{
    public interface ITile
    {
        public TileView TileView { get; }
        public TileModel TileModel { get; set; }
    }
}