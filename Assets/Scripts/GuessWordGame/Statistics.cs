using GuessWordGame;
using System.Collections.Generic;

namespace Assets.Scripts.GuessWordGame
{
    public class Statistics
    {
        private readonly Dictionary<DifficultyType, (int Wins, int Loses, float WinPercent)> _playedGameStatisticsByDifficulty
            = new();
        public IReadOnlyDictionary<DifficultyType, (int Wins, int Loses, float WinPercent)> PlayedGameStatisticsByDifficulty
            => _playedGameStatisticsByDifficulty;

        public void SetPlayedGame(DifficultyType difficultyType, bool isWin)
        {
            _playedGameStatisticsByDifficulty.TryGetValue(difficultyType, out var statistics);
            // Возможно проценты попед и, возможно, проигрыши нужно было перенести в UI слой и вычислять находу,
            // так как тут эти данные по факту не нужны. Тут можно было оставить победы и все сыгранные игры.
            // Но раз уж написал, то пусть будут тут.
            if (isWin)
            {
                statistics.Wins++;
            }
            else
            {
                statistics.Loses++;
            }

            statistics.WinPercent = statistics.Loses != 0 ? statistics.Wins / statistics.Loses : 1f;

            _playedGameStatisticsByDifficulty[difficultyType] = statistics;
        }
    }
}
