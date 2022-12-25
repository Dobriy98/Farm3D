using System;
using Common;
using Core;
using Counters;
using Crops.States;
using Crops.Interfaces;
using Tiles;

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
        
        private Fsm<Crop> _fsm;
        
        private const string CollectSignal = "Collect";

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
            
            _fsm = new Fsm<Crop>();
            InitializeStates();
        }

        public void Collect()
        {
            _fsm.Signal(CollectSignal);
        }

        public void Plant()
        {
            _fsm.ChangeState<CropGrowingState>();
        }
        public void Tick(float deltaTime)
        {
            _fsm.DoUpdate();
        }

        public void Destroy()
        {
            _updater.RemoveListener(this);
            _onDestroyHandler -= Destroy;
        }

        private void InitializeStates()
        {
            CropInit cropInit = new CropInit(this, _updater, _onDestroyHandler);
            CropGrowingState cropGrowingState = new CropGrowingState(CropModel, _cropViewStatesConfigs, _tile);
            CropReady cropReady = new CropReady(CropModel, _cropCounter,
                _experienceCounter, _onDestroyHandler);
            
            _fsm.AddState<CropInit>(cropInit);
            _fsm.AddState<CropGrowingState>(cropGrowingState);
            _fsm.AddState<CropReady>(cropReady);
            
            _fsm.ChangeState<CropInit>();
        }
    }
}