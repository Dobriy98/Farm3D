using UnityEngine;
using System.Linq;
using Tiles;
using static Common.Fsm;

namespace Crops.States
{
    public class CropGrowingState : AState
    {
        private readonly CropModel _cropModel;
        private readonly ITile _tile;

        private CropViewStatesConfig _currentCropState;
        private CropViewStatesConfig[] _cropStatesConfigs;
        private CropView _currenCropView;
        
        private float _timeToGrowUp;
        private int _cropStateIndex;
        
        private const float OneHundredPercent = 100;

        public CropGrowingState(CropModel cropModel, CropViewStatesConfig[] cropViewStatesConfigs, ITile tile)
        {
            _cropModel = cropModel;
            _cropStatesConfigs = cropViewStatesConfigs;
            _tile = tile;
        }
        
        public override void Enter()
        {
            if (_cropStatesConfigs.Length == 0)
            {
                Debug.LogWarning("There is no CropViewStatesConfigs on " + _currenCropView);
            }
            _timeToGrowUp = 0;
            _cropStateIndex = 0;
            _cropStatesConfigs = _cropStatesConfigs.OrderBy(crop => crop.percentToSetView).ToArray();
            _currentCropState = _cropStatesConfigs[0];
        }

        public override void Update()
        {
            _timeToGrowUp += Time.deltaTime;
            
            float percents = _timeToGrowUp * OneHundredPercent / _cropModel.ripeningTime;
            if (percents >= _currentCropState.percentToSetView)
            {
                if (_cropStateIndex < _cropStatesConfigs.Length)
                {
                    CreateCropView();
                    _cropStateIndex++;
                }
            }

            if (percents >= OneHundredPercent)
            {
                Fsm.ChangeState<CropReady, CropView>(_currenCropView);
            }
        }

        private void CreateCropView()
        {
            if(_currenCropView != null) Object.Destroy(_currenCropView.gameObject);
            
            var cropView = Object.Instantiate(_cropStatesConfigs[_cropStateIndex].cropViewStatePrefab, 
                _tile.TileView.transform.position,
                Quaternion.identity);
            
            cropView.transform.SetParent(_tile.TileView.transform);

            _currenCropView = cropView;

            var nextCropStateIndex = _cropStateIndex + 1;
            if (nextCropStateIndex < _cropStatesConfigs.Length)
            {
                _currentCropState = _cropStatesConfigs[nextCropStateIndex];
            }
        }
    }
}