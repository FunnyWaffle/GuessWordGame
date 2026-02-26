using GuessWordGame;
using System.Collections.Generic;

namespace Assets.Scripts.GuessWordGame
{
    public static class Config
    {
        private static readonly Dictionary<DifficultyType, (int MinLeght, int MaxLenght)> _wordLenghtByDifficulty = new()
        {
            [DifficultyType.Easy] = (0, 5),
            [DifficultyType.Medium] = (4, 6),
            [DifficultyType.Hard] = (5, int.MaxValue),
        };
        private static readonly Dictionary<DifficultyType, int> _attemptsCountByDifficulty = new()
        {
            [DifficultyType.Easy] = 10,
            [DifficultyType.Medium] = 9,
            [DifficultyType.Hard] = 8,
        };
        public static IReadOnlyDictionary<DifficultyType, (int MinLeght, int MaxLenght)> WordLenghtByDifficulty => _wordLenghtByDifficulty;
        public static IReadOnlyDictionary<DifficultyType, int> AttemptsCountByDifficulty => _attemptsCountByDifficulty;
    }
}
