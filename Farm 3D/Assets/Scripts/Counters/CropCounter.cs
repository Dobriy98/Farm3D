using System.Collections.Generic;
using Crops;
using UI;

namespace Counters
{
    public class CropCounter
    {
        private readonly GameplayCanvas _gameplayCanvas;
        private readonly Dictionary<CropType, int> _cropsDictionary = new Dictionary<CropType, int>();

        public CropCounter(GameplayCanvas gameplayCanvas)
        {
            _gameplayCanvas = gameplayCanvas;
        }

        public void AddTo(CropType type, int value)
        {
            if (_cropsDictionary.ContainsKey(type))
            {
                _cropsDictionary[type] += value;
            }
            else
            {
                _cropsDictionary.Add(type, value);
            }
            
            AddToUI(type);
        }

        private void AddToUI(CropType cropType)
        {
            switch (cropType)
            {
                case CropType.Carrot:
                    _cropsDictionary.TryGetValue(cropType, out var count);
                    _gameplayCanvas.CarrotCount.Value = count.ToString();
                    break;
            }
        }
    }
}