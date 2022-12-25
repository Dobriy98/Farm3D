using Counters;
using Crops;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayCanvas : CanvasModel
    {
        [SerializeField] private TextMeshProUGUI carrotCount;
        [SerializeField] private TextMeshProUGUI expCount;

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
            if (cropType == CropType.Carrot) carrotCount.text = value.ToString();
        }

        private void UpdateExperienceValue(int value)
        {
            expCount.text = value.ToString();
        }

        private void OnDestroy()
        {
            _cropCounter.OnCropValueChanged -= UpdateCropValue;
            _experienceCounter.OnExperienceValueChanged -= UpdateExperienceValue;
        }
    }
}