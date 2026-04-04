using System;

namespace Assets.Scripts.ThirdPersonGame.Core.Minigames
{
    public class DanceScore
    {
        private const int SPECIALSCOREVALUE = 50;
        private const int SPECIALSCOREVALUEMULTIPLIER = 2;

        private int _score;
        private int _currentSpecialScoreValue;

        public event Action<int> ScoreChanged;
        public event Action SpecialScoreValueReached;

        public DanceScore()
        {
            _currentSpecialScoreValue = SPECIALSCOREVALUE;
        }

        public void AddScoreValue(int value)
        {
            _score += value;
            ScoreChanged?.Invoke(_score);

            if (_score >= _currentSpecialScoreValue)
            {
                _currentSpecialScoreValue *= SPECIALSCOREVALUEMULTIPLIER;
                SpecialScoreValueReached?.Invoke();
            }
        }

        public void Reset()
        {
            _score = 0;
            _currentSpecialScoreValue = SPECIALSCOREVALUE;
        }
    }
}
