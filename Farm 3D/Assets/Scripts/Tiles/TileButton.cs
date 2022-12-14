using System;
using Crops;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Tiles
{
    [RequireComponent(typeof(Image))]
    public class TileButton : MonoBehaviour, IMouseUIClick
    {
        public event Action<CropType> OnTileButtonClick; 

        private Image _tileButtonImage;
        
        private CropType _cropType;

        public void Initialize(Sprite tileButtonSprite, CropType cropType)
        {
            _tileButtonImage = GetComponent<Image>();
            _tileButtonImage.sprite = tileButtonSprite;
            _cropType = cropType;
        }
        public void Click()
        {
            OnTileButtonClick?.Invoke(_cropType);
        }
    }
}