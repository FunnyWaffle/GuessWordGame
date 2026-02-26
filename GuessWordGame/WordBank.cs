namespace GuessWordGame
{
    public class WordBank
    {
        private readonly Dictionary<DifficultyType, List<Word>> _words = new();
        public WordBank(IReadOnlyDictionary<DifficultyType, (int, int)> difficultySettings)
        {
            var allWords = new List<string>() { "Home", "Kid", "Boy", "Girl", "Gal", "Ship", "Robber", "Mariner", "Leviathan",
                "Couple", "Stream", "Warning", "Difficulty", "Tavern", "Settings", "Dictionary" };

            SortWordsByDifficulty(allWords, difficultySettings);
        }
        private void SortWordsByDifficulty(List<string> words, IReadOnlyDictionary<DifficultyType, (int, int)> difficultySettings)
        {
            foreach (var word in words)
            {
                var wordLength = word.Length;
                foreach (var difficulty in GetWordDifficultyByLenght(wordLength, difficultySettings))
                {
                    AddWordToBank(word, difficulty);
                }
            }
        }
        private IEnumerable<DifficultyType> GetWordDifficultyByLenght(int wordLenght,
            IReadOnlyDictionary<DifficultyType, (int Min, int Max)> difficultySettings)
        {
            foreach (var (difficulty, settings) in difficultySettings)
            {
                if (wordLenght >= settings.Min && wordLenght <= settings.Max)
                    yield return difficulty;
            }
        }
        private void AddWordToBank(string wordValue, DifficultyType difficulty)
        {
            if (!_words.TryGetValue(difficulty, out var words))
            {
                words = new List<Word>();
                _words[difficulty] = words;
            }

            var word = new Word(wordValue);
            words.Add(word);
        }

        public bool TryGenerateWordByDifficulty(DifficultyType difficultyType, out Word word)
        {
            var wasWordFound = false;

            if (!_words.TryGetValue(difficultyType, out var words))
            {
                word = null;
                return wasWordFound;
            }

            var random = new Random();
            var wordIndex = random.Next(0, words.Count);

            word = words[wordIndex];
            wasWordFound = true;

            return wasWordFound;
        }
    }
}
