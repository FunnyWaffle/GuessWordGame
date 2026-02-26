namespace GuessWordGame
{
    public class LettersBank
    {
        private readonly HashSet<char> _guessedLetters = new();
        private readonly HashSet<char> _failedLetters = new();

        public IEnumerable<char> GuessedLetters => _guessedLetters;
        public IEnumerable<char> FailedLetters => _failedLetters;

        public event Action<IEnumerable<char>> GuessedLettersChanged;
        public event Action<IEnumerable<char>> FailedLettersChanged;
        public void AddGuessedLetter(char letter)
        {
            _guessedLetters.Add(letter);
            GuessedLettersChanged?.Invoke(_guessedLetters);
        }
        public void AddFailedLetter(char letter)
        {
            _failedLetters.Add(letter);
            FailedLettersChanged?.Invoke(_failedLetters);
        }

        public void Reset()
        {
            _guessedLetters.Clear();
            GuessedLettersChanged?.Invoke(_guessedLetters);

            _failedLetters.Clear();
            FailedLettersChanged?.Invoke(_failedLetters);
        }
    }
}
