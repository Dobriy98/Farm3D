using System;
using System.Collections.Generic;
using Crops;
using UI;

namespace Counters
{
    public class CropCounter
    {
        public event Action<CropType, int> OnCropValueChanged;
        
        private readonly Dictionary<CropType, int> _cropsDictionary = new Dictionary<CropType, int>();

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
            
            OnCropValueChanged?.Invoke(type, _cropsDictionary[type]);
        }
    }
}