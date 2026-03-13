using System;
using System.Collections.Generic;

namespace Assets.Scripts.GuessWordGame
{
    public class Statistics
    {
        private readonly Dictionary<Difficulty, DifficultyStatistics> _playedGameStatisticsByDifficulty
            = new();

        public Statistics()
        {
            CreateStatistics();
        }

        public event Action<Difficulty, DifficultyStatistics> DifficultyStatisticsChanged;

        private void CreateStatistics()
        {
            var difficulties = Config.DifficultyConfigs;
            foreach (var (difficulty, _) in difficulties)
            {
                _playedGameStatisticsByDifficulty[difficulty] = new DifficultyStatistics();
            }
        }

        public void SetPlayedGame(Difficulty difficultyType, bool isWin)
        {
            var statistics = _playedGameStatisticsByDifficulty[difficultyType];
            if (isWin)
            {
                statistics.WinsCount++;
            }

            statistics.PlayedGamesCount++;

            DifficultyStatisticsChanged?.Invoke(difficultyType, statistics);
        }
    }
}
