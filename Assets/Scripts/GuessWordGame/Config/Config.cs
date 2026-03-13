using System.Collections.Generic;

namespace Assets.Scripts.GuessWordGame
{
    public static class Config
    {
        private static readonly Dictionary<Difficulty, DifficultyConfig> _difficultyConfigs = new()
        {
            [Difficulty.Easy] = new DifficultyConfig(minWordLenght: 0, maxWordLenght: 5, guessLetterAttemtsCount: 10,
                lifeCount: 10),
            [Difficulty.Medium] = new DifficultyConfig(minWordLenght: 4, maxWordLenght: 6, guessLetterAttemtsCount: 9,
                lifeCount: 7),
            [Difficulty.Hard] = new DifficultyConfig(minWordLenght: 5, maxWordLenght: int.MaxValue, guessLetterAttemtsCount: 8,
                lifeCount: 5),
        };

        public static IReadOnlyDictionary<Difficulty, DifficultyConfig> DifficultyConfigs => _difficultyConfigs;
    }
}
