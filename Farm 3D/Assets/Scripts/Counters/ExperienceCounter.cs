using UI;

namespace Counters
{
    public class ExperienceCounter
    {
        private readonly GameplayCanvas _gameplayCanvas;
        private int _experienceCount = 0;
        
        public ExperienceCounter(GameplayCanvas gameplayCanvas)
        {
            _gameplayCanvas = gameplayCanvas;
        }

        public void AddExp(int value)
        {
            _experienceCount += value;
            _gameplayCanvas.ExpCount.Value = _experienceCount.ToString();
        }
    }
}