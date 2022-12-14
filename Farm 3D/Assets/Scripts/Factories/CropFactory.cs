using System;
using Core;
using Counters;
using Crops;
using Factories.Interfaces;

namespace Factories
{
    public class CropFactory : ICropFactory
    {
        private readonly CropsConfigs _cropsConfigs;
        private readonly Updater _updater;
        private readonly CropCounter _cropCounter;
        private readonly ExperienceCounter _experienceCounter;
        public CropFactory(CropsConfigs cropConfigs, Updater updater, CropCounter cropCounter, ExperienceCounter experienceCounter)
        {
            _cropsConfigs = cropConfigs;
            _updater = updater;
            _cropCounter = cropCounter;
            _experienceCounter = experienceCounter;
        }
        public Crop Create(CropType type)
        {
            if (!TryFindConfig(type, out var cropConfig))
            {
                throw new NullReferenceException("There is no CropConfig in CropFactory");
            }

            Crop crop = new Crop(cropConfig.cropModel, cropConfig.cropViewStatesConfig, _updater, _cropCounter,
                _experienceCounter);

            return crop;
        }

        private bool TryFindConfig(CropType type, out CropConfig config)
        {
            foreach (var cropConfig in _cropsConfigs.cropConfigs)
            {
                if (cropConfig.cropModel.cropType == type)
                {
                    config = cropConfig;
                    return true;
                }
            }

            config = null;
            return false;
        }
    }
}