namespace Tiles
{
    public interface ITile
    {
        public TileView TileView { get; set; }
        public TileModel TileModel { get; set; }
    }
}