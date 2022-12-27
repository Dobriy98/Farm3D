using System;
using Common;
using Core;
using Counters;
using Crops.States;
using Crops.Interfaces;
using Tiles;
using Tiles.States;

namespace Crops
{
    public class Crop : ICrop, IUpdateListener
    {
        public CropModel CropModel { get; }
        
        private readonly CropViewStatesConfig[] _cropViewStatesConfigs;
        private readonly Updater _updater;
        
        private readonly CropCounter _cropCounter;
        private readonly ExperienceCounter _experienceCounter;
        
        private Action _onDestroyHandler;

        private ITile _tile;
        
        private Fsm _fsm;

        public float RipeningTime => CropModel.ripeningTime;

        public Crop(CropModel cropModel, CropViewStatesConfig[] cropViewStatesConfigs, Updater updater,
            CropCounter cropCounter, ExperienceCounter experienceCounter)
        {
            CropModel = cropModel;
            _cropViewStatesConfigs = cropViewStatesConfigs;

            _updater = updater;
            _cropCounter = cropCounter;
            _experienceCounter = experienceCounter;
        }

        public void Initialize(ITile tile)
        {
            _tile = tile;
            _onDestroyHandler += Destroy;
            _updater.AddListener(this);
            
            _fsm = new Fsm();
            InitializeStates();
        }

        public void Collect()
        {
            if(!_fsm.CompareState<CropReady>()) return;

            CropReady cropReady = _fsm.TakeState<CropReady>();
            cropReady.CollectingSignal();
        }

        public void Plant()
        {
            _fsm.ChangeState<CropGrowingState>();
        }
        public void Tick(float deltaTime)
        {
            _fsm.DoUpdate();
        }

        private void Destroy()
        {
            _updater.RemoveListener(this);
            _onDestroyHandler -= Destroy;
        }

        private void InitializeStates()
        {
            CropGrowingState cropGrowingState = new CropGrowingState(CropModel, _cropViewStatesConfigs, _tile);
            CropReady cropReady = new CropReady(CropModel, _cropCounter,
                _experienceCounter, _onDestroyHandler);
            
            _fsm.AddState<CropGrowingState>(cropGrowingState);
            _fsm.AddState<CropReady>(cropReady);
        }
    }
}