using Counters;
using Crops;

namespace UI
{
    public class GameplayCanvas : CanvasModel
    {
        public readonly ObservableUI<string> ExpCount = new ObservableUI<string>();
        public readonly ObservableUI<string> CarrotCount = new ObservableUI<string>();

        private CropCounter _cropCounter;
        private ExperienceCounter _experienceCounter;

        public void Initialize(CropCounter cropCounter, ExperienceCounter experienceCounter)
        {
            _cropCounter = cropCounter;
            _experienceCounter = experienceCounter;
            
            _cropCounter.OnCropValueChanged += UpdateCropValue;
            _experienceCounter.OnExperienceValueChanged += UpdateExperienceValue;
        }

        private void UpdateCropValue(CropType cropType, int value)
        {
            if (cropType == CropType.Carrot) CarrotCount.Value = value.ToString();
        }

        private void UpdateExperienceValue(int value)
        {
            ExpCount.Value = value.ToString();
        }

        private void OnDestroy()
        {
            _cropCounter.OnCropValueChanged -= UpdateCropValue;
            _experienceCounter.OnExperienceValueChanged -= UpdateExperienceValue;
        }
    }
}