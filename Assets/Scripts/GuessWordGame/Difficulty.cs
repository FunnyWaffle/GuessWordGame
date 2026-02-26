using GuessWordGame;
using System;

namespace Assets.Scripts.GuessWordGame
{
    public class Difficulty
    {
        private DifficultyType? _currentValue;

        public DifficultyType? CurrentValue
        {
            get => _currentValue;
            private set
            {
                DufficultyChanged?.Invoke(value);
                _currentValue = value;
            }
        }

        public event Action<DifficultyType?> DufficultyChanged;
        public event Action<string> DufficultyChangeFailed;

        public void Reset()
        {
            CurrentValue = null;
        }
        public bool TrySetDifficulty(string type)
        {
            bool wasSetted;

            if (TryGetDifficultyTypeByIndex(type, out var difficultyType)
                || TryGetDifficultyTypeEnumValue(type, out difficultyType))
            {
                CurrentValue = difficultyType;
                wasSetted = true;
            }
            else
            {
                CurrentValue = null;
                wasSetted = false;
                DufficultyChangeFailed?.Invoke(type);
            }

            return wasSetted;
        }
        private bool TryGetDifficultyTypeByIndex(string type, out DifficultyType? difficultyType)
        {
            var wasConverted = false;

            var firstSymbol = type[0];
            difficultyType = firstSymbol switch
            {
                '1' => (DifficultyType?)DifficultyType.Easy,
                '2' => (DifficultyType?)DifficultyType.Medium,
                '3' => (DifficultyType?)DifficultyType.Hard,
                _ => null,
            };

            if (difficultyType.HasValue)
                wasConverted = true;

            return wasConverted;
        }
        private bool TryGetDifficultyTypeEnumValue(string type, out DifficultyType? difficultyType)
        {
            var wasParsed = Enum.TryParse(type, true, out DifficultyType parsedDifficultyType)
                && Enum.IsDefined(typeof(DifficultyType), parsedDifficultyType);

            if (wasParsed)
                difficultyType = parsedDifficultyType;
            else
                difficultyType = null;

            return wasParsed;
        }
    }
}
