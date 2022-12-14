using Tiles;

namespace Factories.Interfaces
{
    public interface ITileFactory: IFactory<Tile>
    {
        public TileView TakeTilePrefab();
    }
}