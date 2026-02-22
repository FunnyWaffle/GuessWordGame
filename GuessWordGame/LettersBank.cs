namespace GuessWordGame
{
    public class LettersBank
    {
        private readonly HashSet<char> _guessedLetters = new();
        private readonly HashSet<char> _failedLetters = new();

        public IEnumerable<char> GuessedLetters => _guessedLetters;
        public IEnumerable<char> FailedLetters => _failedLetters;

        public void AddGuessedLetter(char letter)
        {
            _guessedLetters.Add(letter);
        }
        public void AddFailedLetter(char letter)
        {
            _failedLetters.Add(letter);
        }

        public void Clear()
        {
            _guessedLetters.Clear();
            _failedLetters.Clear();
        }
    }
}
