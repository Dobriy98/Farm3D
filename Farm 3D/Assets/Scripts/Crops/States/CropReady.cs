using System;
using Common;
using Counters;
using UnityEngine;
using static Common.Fsm<Crops.Crop>;
using Object = UnityEngine.Object;

namespace Crops.States
{
    public class CropReady : AState<CropView>, ISignalHandler<string>
    {
        private readonly CropModel _cropModel;
        private readonly CropCounter _cropCounter;
        private readonly ExperienceCounter _experienceCounter;
        private readonly Action _onDestroyHandler;

        private CropView _currentCropView;
        
        public CropReady(CropModel cropModel, CropCounter cropCounter, 
            ExperienceCounter experienceCounter, Action onDestroyHandler)
        {
            _cropModel = cropModel;
            _cropCounter = cropCounter;
            _experienceCounter = experienceCounter;
            _onDestroyHandler = onDestroyHandler;
        }
        public override void SetStateArg(CropView arg)
        {
            _currentCropView = arg;
        }
        public override void Enter()
        {
            int expCount = Mathf.FloorToInt(_cropModel.ripeningTime);
            _experienceCounter.AddExp(expCount);
        }

        public void Signal(string signal)
        {
            switch (signal)
            {
                case "Collect":
                    _onDestroyHandler?.Invoke();
                    _cropCounter.AddTo(_cropModel.cropType, _cropModel.pointsToAdd);
                    Object.Destroy(_currentCropView.gameObject);
                    break;
            }
        }

    }
}