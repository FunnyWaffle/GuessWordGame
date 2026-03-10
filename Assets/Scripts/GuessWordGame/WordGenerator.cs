using System.Collections.Generic;

namespace Assets.Scripts.GuessWordGame
{
    public class WordGenerator
    {
        private readonly Dictionary<int, List<Word>> _wordsByLenght = new();

        private int _maxWordLenght;
        public WordGenerator()
        {
            var allWords = new List<string>() { "Home", "Kid", "Boy", "Girl", "Gal", "Ship", "Robber", "Mariner", "Leviathan",
                "Couple", "Stream", "Warning", "Difficulty", "Tavern", "Settings", "Dictionary" };

            SortWordsByDifficulty(allWords);
        }
        private void SortWordsByDifficulty(List<string> words)
        {
            foreach (var word in words)
            {
                AddWordToBank(word);
            }
        }
        private void AddWordToBank(string wordValue)
        {
            var wordLenght = wordValue.Length;
            if (!_wordsByLenght.TryGetValue(wordLenght, out var words))
            {
                words = new List<Word>();
                _wordsByLenght[wordLenght] = words;
            }

            var word = new Word(wordValue);
            words.Add(word);

            if (wordLenght > _maxWordLenght)
                _maxWordLenght = wordLenght;
        }

        public bool TryGenerateWordByLenght(int minLenght, int maxLenght, out Word word)
        {
            word = null;

            if (!TryGetWords(minLenght, maxLenght > _maxWordLenght ? _maxWordLenght : maxLenght, out var words))
                return false;

            var wordIndex = UnityEngine.Random.Range(0, words.Count);
            word = words[wordIndex];

            return true;
        }
        private bool TryGetWords(int minWordLenght, int maxWordLenght, out List<Word> words)
        {
            var iterationsCount = maxWordLenght - minWordLenght;
            for (int i = 0; i < iterationsCount; i++)
            {
                var lenght = UnityEngine.Random.Range(minWordLenght, maxWordLenght);

                if (_wordsByLenght.TryGetValue(lenght, out words))
                {
                    return true;
                }
            }

            words = null;
            return false;
        }
    }
}
