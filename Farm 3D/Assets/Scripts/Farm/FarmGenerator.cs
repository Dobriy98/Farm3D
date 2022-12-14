using Factories.Interfaces;
using Tiles;
using UnityEngine;
using UnityEngine.AI;

namespace Farm
{
    public class FarmGenerator: IFarmGeneration
    {
        private readonly int _rows;
        private readonly int _columns;

        private readonly float _offsetX;
        private readonly float _offsetZ;
        
        private readonly ITileFactory _tileFactory;

        public FarmGenerator(FarmGenerationConfig config, ITileFactory tileFactory)
        {
            var tilePrefab = tileFactory.TakeTilePrefab();
            _rows = config.rows;
            _columns = config.columns;

            Vector3 localScale = tilePrefab.transform.localScale;
            _offsetX = localScale.x;
            _offsetZ = localScale.z;

            _tileFactory = tileFactory;
        }
        
        public void Generate()
        {
            GameObject parent = new GameObject("Garden");
            for (int i = 0; i < _columns * _rows; i++)
            {
                float xPosition = _offsetX * (i % _columns);
                float zPosition = _offsetZ * (i / _columns);
                Vector3 positionToCreate = new Vector3(xPosition, 0, zPosition);
                
                Tile tile = _tileFactory.Create();
                tile.TileView.transform.position = positionToCreate;
                tile.TileView.transform.SetParent(parent.transform);
                tile.Initialize();
            }

            NavMeshSurface parentSurface = parent.AddComponent<NavMeshSurface>();
            parentSurface.BuildNavMesh();
        }
    }
}