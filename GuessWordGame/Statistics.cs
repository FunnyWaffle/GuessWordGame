namespace GuessWordGame
{
    public class Statistics
    {
        private readonly Dictionary<DifficultyType, int> _playedGamesCountByDifficulty = new();
        public IReadOnlyDictionary<DifficultyType, int> PlayedGamesCountByDifficulty => _playedGamesCountByDifficulty;

        public void SetPlayedGame(DifficultyType difficultyType)
        {
            _playedGamesCountByDifficulty.TryGetValue(difficultyType, out var count);
            _playedGamesCountByDifficulty[difficultyType] = ++count;
        }
    }
}
