using Assets.Scripts.GuessWordGame;

namespace GuessWordGame
{
    public class Game
    {
        private readonly GameUI _gameUI;
        private readonly Difficulty _difficulty;
        private readonly WordBank _wordBank;
        private readonly LettersBank _lettersBank;
        private readonly Attempts _attempts;
        private readonly LetterGuesser _letterGuesser;
        private readonly Statistics _statistics;

        private bool _isDiffuicultyChosen = false;
        private bool _isWordGenerated = false;
        private int _guessedWordsCount;

        public Game()
        {
            _gameUI = new GameUI();
            _difficulty = new Difficulty();

            _wordBank = new WordBank(Config.WordLenghtByDifficulty);
            _lettersBank = new LettersBank();
            _attempts = new Attempts();
            _letterGuesser = new LetterGuesser();
            _statistics = new Statistics();

            InitializeEvents();

            _difficulty.Reset();
        }
        private void InitializeEvents()
        {
            Input.EnterPressed += HandleInput;

            _difficulty.DufficultyChanged += _gameUI.HandleDifficultyChange;
            _difficulty.DufficultyChangeFailed += _gameUI.HandleDifficultyChangeFail;

            _attempts.AttemptsCountChanged += _gameUI.HandleAttemptsCountChange;

            _lettersBank.GuessedLettersChanged += _gameUI.HandleGuessedLettersChage;
            _lettersBank.FailedLettersChanged += _gameUI.HandleFailedLettersChage;

            _letterGuesser.MaskChanged += _gameUI.HandleMaskChange;
        }
        private void HandleInput(string input)
        {
            if (!_isDiffuicultyChosen)
            {
                ChooseDifficulty(input);
            }

            if (_isDiffuicultyChosen && !_isWordGenerated)
            {
                TryGenerateWord();
                return;
            }

            if (!_isWordGenerated)
                return;

            TryGuessWordLetter(input);

            if (_letterGuesser.IsWordGuessed(_lettersBank.GuessedLetters))
            {
                _guessedWordsCount++;
                _gameUI.PrintGuessedWordsCount(true, _guessedWordsCount);

                Restart(true);
                return;
            }
            else
            {
                _gameUI.PrintGuessedWordsCount(false, _guessedWordsCount);
            }

            if (_attempts.Count <= 0)
            {
                Restart(false);
            }
        }
        private void ChooseDifficulty(string input)
        {
            if (input == string.Empty)
                return;

            if (_difficulty.TrySetDifficulty(input))
            {
                _isDiffuicultyChosen = true;
                _attempts.Count = Config.AttemptsCountByDifficulty[_difficulty.CurrentValue!.Value];
            }
        }
        private void TryGenerateWord()
        {
            _isWordGenerated = _wordBank.TryGenerateWordByDifficulty(_difficulty.CurrentValue!.Value, out var word);
            _letterGuesser.Word = word;
        }
        private bool TryGuessWordLetter(string input)
        {
            var letter = input[0];
            if (_letterGuesser.TryGuessLetter(letter, _lettersBank.GuessedLetters))
            {
                _lettersBank.AddGuessedLetter(letter);
                return true;
            }
            else
            {
                _lettersBank.AddFailedLetter(letter);
            }

            _attempts.Count--;

            return false;
        }
        private void Restart(bool isWin)
        {
            _statistics.SetPlayedGame(_difficulty.CurrentValue!.Value, isWin);
            _gameUI.PrintPlayedGamesStatistics(_statistics.PlayedGameStatisticsByDifficulty);

            _difficulty.Reset();
            _attempts.Reset();
            _lettersBank.Reset();
            _letterGuesser.Reset();
            Reset();
        }
        private void Reset()
        {
            _isDiffuicultyChosen = false;
            _isWordGenerated = false;
        }
    }
}
