using System;
using UI;

namespace Counters
{
    public class ExperienceCounter
    {
        public event Action<int> OnExperienceValueChanged;
        private int _experienceCount;

        public void AddExp(int value)
        {
            _experienceCount += value;
            OnExperienceValueChanged?.Invoke(_experienceCount);
        }
    }
}