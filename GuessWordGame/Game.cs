namespace GuessWordGame
{
    public class Game
    {
        private readonly GameUI _gameUI;
        private readonly Difficulty _difficulty;
        private readonly WordBank _wordBank;
        private readonly LettersBank _lettersBank;

        private Word _word;
        private bool _isDiffuicultyChosen = false;
        private bool _isWordGenerated = false;
        private int _attemptsCount;
        private int _guessedWordsCount;
        public Game()
        {
            _gameUI = new GameUI();
            _gameUI.SetStartSettingDificultyUIElementActive(true);

            _difficulty = new Difficulty();
            Input.EnterPressed += HandleInput;

            _wordBank = new WordBank(_difficulty.WordSettings);
            _lettersBank = new LettersBank();
        }
        private void HandleInput(string input)
        {
            if (!_isDiffuicultyChosen)
            {
                ChooseDifficulty(input);
            }

            if (_isDiffuicultyChosen && !_isWordGenerated)
            {
                _isWordGenerated = _wordBank.TryGenerateWordByDifficulty(_difficulty.CurrentValue!.Value, out _word);

                if (_isWordGenerated)
                {
                    _gameUI.PrintMask(true, _word.GetMask(_lettersBank.GuessedLetters));
                    _gameUI.PrintGuessedLetters(true, string.Concat(_lettersBank.GuessedLetters));
                    _gameUI.PrintFailedLetters(true, string.Concat(_lettersBank.FailedLetters));
                }
                return;
            }

            if (_isWordGenerated)
                if (TryGuessWordLetter(input))
                {
                    if (_attemptsCount <= 0)
                    {
                        _gameUI.PrintLoseMessage(true);
                    }
                    _guessedWordsCount++;
                    _gameUI.PrintGuessedWordsCount(true, _guessedWordsCount);
                }
        }
        private void ChooseDifficulty(string input)
        {
            if (input == string.Empty)
                return;

            if (_difficulty.TrySetDifficulty(input))
            {
                _attemptsCount = _difficulty.AttemptSettings[_difficulty.CurrentValue!.Value];

                _isDiffuicultyChosen = true;

                _gameUI.SetStartSettingDificultyUIElementActive(false);
                _gameUI.PrintDifficulty(true, _difficulty.CurrentValue!.Value.ToString());
                _gameUI.ShowFailSettingDificultyUIElement(false);
                _gameUI.SetLeftAttemptsUIElementState(true, _attemptsCount);
            }
            else
            {

                _gameUI.ShowFailSettingDificultyUIElement(true, input);
                _gameUI.SetStartSettingDificultyUIElementActive(true);
                _gameUI.PrintDifficulty(false);
                _gameUI.SetLeftAttemptsUIElementState(false, 0);
            }
        }
        private bool TryGuessWordLetter(string input)
        {
            var letter = input[0];
            if (_word.Contains(letter))
            {
                _lettersBank.AddGuessedLetter(letter);
                _gameUI.PrintGuessedLetters(true, string.Concat(_lettersBank.GuessedLetters));
            }
            else
            {
                _attemptsCount--;
                _lettersBank.AddFailedLetter(letter);
                _gameUI.PrintFailedLetters(true, string.Concat(_lettersBank.FailedLetters));
                _gameUI.SetLeftAttemptsUIElementState(true, _attemptsCount);
            }

            var mask = _word.GetMask(_lettersBank.GuessedLetters);
            _gameUI.PrintMask(true, mask);

            return !mask.Contains('*');
        }
    }
}
