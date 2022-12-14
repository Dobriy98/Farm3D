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
        public CropModel CropModel { get; set; }
        public CropView CurrentCropView;
        
        public readonly CropViewStatesConfig[] CropViewStatesConfigs;
        public readonly Updater Updater;
        
        public readonly CropCounter CropCounter;
        public readonly ExperienceCounter ExperienceCounter;

        public Action OnDestroyHandler;

        public ITile Tile;
        
        private Fsm<Crop> _fsm;
        
        private const string CollectSignal = "Collect";

        public float RipeningTime
        {
            get => CropModel.ripeningTime;
            set => CropModel.ripeningTime = value;
        }

        public Crop(CropModel cropModel, CropViewStatesConfig[] cropViewStatesConfigs, Updater updater,
            CropCounter cropCounter, ExperienceCounter experienceCounter)
        {
            CropModel = cropModel;
            CropViewStatesConfigs = cropViewStatesConfigs;

            Updater = updater;
            CropCounter = cropCounter;
            ExperienceCounter = experienceCounter;
        }

        public void Initialize(ITile tile)
        {
            Tile = tile;
            _fsm = new Fsm<Crop>(this, new CropInit());
        }

        public void Collect()
        {
            _fsm.Signal(CollectSignal);
        }

        public void Plant()
        {
            _fsm.ChangeState(new CropGrowingState());
        }
        public void Tick(float deltaTime)
        {
            _fsm.DoUpdate();
        }

        public void Destroy()
        {
            Updater.RemoveListener(this);
            OnDestroyHandler -= Destroy;
        }
    }
}