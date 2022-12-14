using UnityEngine;
using System.Linq;
using static Common.Fsm<Crops.Crop>;

namespace Crops.States
{
    public class CropGrowingState : AState
    {
        private CropViewStatesConfig _currentCropState;
        private CropViewStatesConfig[] _cropStatesConfigs;
        
        private float _timeToGrowUp;
        private int _cropStateIndex;
        
        private const float OneHundredPercent = 100;
        
        public override void Enter()
        {
            if (Context.CropViewStatesConfigs.Length == 0)
            {
                Debug.LogWarning("There is no CropViewStatesConfigs on " + Context);
                Fsm.ChangeState(new CropInit());
            }
            _timeToGrowUp = 0;
            _cropStateIndex = 0;
            _cropStatesConfigs = Context.CropViewStatesConfigs.OrderBy(crop => crop.percentToSetView).ToArray();
            _currentCropState = _cropStatesConfigs[0];
        }

        public override void Update()
        {
            _timeToGrowUp += Time.deltaTime;
            
            float percents = _timeToGrowUp * OneHundredPercent / Context.CropModel.ripeningTime;
            if (percents >= _currentCropState.percentToSetView)
            {
                if (_cropStateIndex < Context.CropViewStatesConfigs.Length)
                {
                    CreateCropView();
                    _cropStateIndex++;
                }
            }

            if (percents >= OneHundredPercent)
            {
                Fsm.ChangeState(new CropReady());
            }
        }

        private void CreateCropView()
        {
            if(Context.CurrentCropView != null) Object.Destroy(Context.CurrentCropView.gameObject);
            
            var cropView = Object.Instantiate(_cropStatesConfigs[_cropStateIndex].cropViewStatePrefab, 
                Context.Tile.TileView.transform.position,
                Quaternion.identity);
            
            cropView.transform.SetParent(Context.Tile.TileView.transform);

            Context.CurrentCropView = cropView;

            var nextCropStateIndex = _cropStateIndex + 1;
            if (nextCropStateIndex < _cropStatesConfigs.Length)
            {
                _currentCropState = _cropStatesConfigs[nextCropStateIndex];
            }
        }
    }
}