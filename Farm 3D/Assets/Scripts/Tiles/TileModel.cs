using System;
using Crops;
using Utils;

namespace Tiles
{
    [Serializable]
    public class TileModel
    {
        public TileUIConfig tileUIConfig;
        public CropConfig[] cropsToSowing;
        public CameraState tileCameraState;
    }
}