using System;
using Core;
using static Common.Fsm<Crops.Crop>;

namespace Crops.States
{
    public class CropInit : AState
    {
        private readonly Crop _crop;
        private readonly Updater _updater;
        
        private  Action _onDestroyHandler;

        public CropInit(Crop crop, Updater updater, Action onDestroyHandler)
        {
            _crop = crop;
            _updater = updater;
            _onDestroyHandler = onDestroyHandler;
        }
        public override void Enter()
        {
            _updater.AddListener(_crop);
            _onDestroyHandler += _crop.Destroy;
        }
    }
}