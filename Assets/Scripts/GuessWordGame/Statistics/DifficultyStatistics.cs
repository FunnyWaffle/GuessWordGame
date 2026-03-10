namespace Assets.Scripts.GuessWordGame
{
    public class DifficultyStatistics
    {
        public int PlayedGamesCount { get; set; }
        public int WinsCount { get; set; }
        public int LosesCount => PlayedGamesCount - WinsCount;
        public float WinRate
        {
            get
            {
                if (WinsCount < 0)
                    return 0;

                return (float)WinsCount / PlayedGamesCount;
            }
        }
    }
}
