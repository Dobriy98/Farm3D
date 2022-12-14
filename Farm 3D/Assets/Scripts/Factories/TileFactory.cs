using Character;
using Character.Interfaces;
using Factories.Interfaces;
using Tiles;
using Utils;
using Object = UnityEngine.Object;

namespace Factories
{
    public class TileFactory : ITileFactory
    {
        private readonly TileConfig _tileConfig;
        private readonly ICharacter _character;
        private readonly ICropFactory _cropFactory;
        private readonly CameraFollow _cameraFollow;
        
        public TileFactory(TileConfig tileConfig, ICharacter character, ICropFactory cropFactory, CameraFollow cameraFollow)
        {
            _tileConfig = tileConfig;
            _character = character;
            _cropFactory = cropFactory;
            _cameraFollow = cameraFollow;
        }
        
        public Tile Create()
        {
            TileView tileView = Object.Instantiate(_tileConfig.tileViewPrefab);

            Tile tile = new Tile(tileView, _tileConfig.tileModel,_character, _cropFactory, _cameraFollow);
            
            return tile;
        }

        public TileView TakeTilePrefab() => _tileConfig.tileViewPrefab;
    }
}