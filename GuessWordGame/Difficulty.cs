namespace GuessWordGame
{
    public class Difficulty
    {
        private readonly Dictionary<DifficultyType, (int MinLeght, int MaxLenght)> _wordSettings = new()
        {
            [DifficultyType.Easy] = (0, 5),
            [DifficultyType.Medium] = (4, 6),
            [DifficultyType.Hard] = (5, int.MaxValue),
        };
        private readonly Dictionary<DifficultyType, int> _attemptSettings = new()
        {
            [DifficultyType.Easy] = 10,
            [DifficultyType.Medium] = 9,
            [DifficultyType.Hard] = 8,
        };

        public IReadOnlyDictionary<DifficultyType, (int MinLeght, int MaxLenght)> WordSettings => _wordSettings;
        public IReadOnlyDictionary<DifficultyType, int> AttemptSettings => _attemptSettings;
        public DifficultyType? CurrentValue { get; private set; }

        public bool TrySetDifficulty(string type)
        {
            bool wasSetted = false;

            if (TryGetDifficultyTypeByIndex(type, out var difficultyType)
                || TryGetDifficultyTypeEnumValue(type, out difficultyType))
            {
                CurrentValue = difficultyType;
                wasSetted = true;
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
                && Enum.IsDefined(parsedDifficultyType);

            if (wasParsed)
                difficultyType = parsedDifficultyType;
            else
                difficultyType = null;

            return wasParsed;
        }
    }
}
