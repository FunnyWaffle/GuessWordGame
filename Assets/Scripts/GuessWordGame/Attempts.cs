using System;

namespace Assets.Scripts.GuessWordGame
{
    public class Attempts
    {
        private int _attemptsCount;
        public int Count
        {
            get => _attemptsCount;
            set
            {
                AttemptsCountChanged?.Invoke(value);
                _attemptsCount = value;
            }
        }
        public event Action<int> AttemptsCountChanged;
        public void Reset()
        {
            Count = -1;
        }
    }
}
