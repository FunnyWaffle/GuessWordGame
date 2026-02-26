namespace GuessWordGame
{
    public class LetterGuesser
    {
        private Word _word;
        public Word Word
        {
            get => _word;
            set
            {
                _word = value;
                MaskChanged?.Invoke(_word.GetMask(Array.Empty<char>()));
            }
        }

        public event Action<string> MaskChanged;
        public bool TryGuessLetter(char letter, IEnumerable<char> guessedLetters)
        {
            var isGuessed = Word.Contains(letter);
            MaskChanged?.Invoke(Word.GetMask(guessedLetters.Append(letter)));
            return isGuessed;
        }
        // Не знаю, что лучше, хранить в этом классе кешированную маску
        // ( но тогда угаданные буквы, по сути, будут дублироваться, два места правды получается)
        // либо тянуть в оба метода угаданные буквы, а маску брать на ходу.
        public bool IsWordGuessed(IEnumerable<char> guessedLetters)
        {
            var mask = Word.GetMask(guessedLetters);
            return !mask.Contains('*');
        }
        public void Reset()
        {
            MaskChanged?.Invoke(string.Empty);
        }
    }
}
