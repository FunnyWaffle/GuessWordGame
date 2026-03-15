namespace Assets.Scripts.GuessWordGame
{
    public class DifficultyConfig
    {
        public DifficultyConfig(int minWordLenght, int maxWordLenght, int guessLetterAttemtsCount, int lifeCount)
        {
            MinWordLenght = minWordLenght;
            MaxWordLenght = maxWordLenght;
            GuessLetterAttemtsCount = guessLetterAttemtsCount;
            LifeCount = lifeCount;
        }

        public int MinWordLenght { get; private set; }
        public int MaxWordLenght { get; private set; }
        public int GuessLetterAttemtsCount { get; private set; }
        public int LifeCount { get; private set; }
    }
}
