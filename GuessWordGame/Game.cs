namespace GuessWordGame
{
    public class Game
    {
        private readonly Difficulty _difficulty;
        private readonly WordBank _wordBank;
        private readonly LettersBank _lettersBank;

        private Word _word;
        private int _guessedWordsCount;
        private int _attemptsCount;
        private bool _isLastDifficultyChosed;
        private bool _hasToChangeDifficulty = true;
        public Game()
        {
            _difficulty = new Difficulty();
            _wordBank = new WordBank(_difficulty.WordSettings);
            _lettersBank = new LettersBank();
        }
        public void Update()
        {
            string mask = _word != null ? _word.GetMask(_lettersBank.GuessedLetters) : string.Empty;
            string choiceDifficultyFeedback = string.Empty;
            UpdateUI(mask, choiceDifficultyFeedback); // нужно обновлять после каждого действия

            if (_hasToChangeDifficulty)
            {
                choiceDifficultyFeedback = ChooseDifficulty();

                _lettersBank.Clear();

                _wordBank.TryGenerateWordByDifficulty(_difficulty.CurrentValue!.Value, out _word);
                mask = _word != null ? _word.GetMask(_lettersBank.GuessedLetters) : string.Empty;

                UpdateUI(mask, choiceDifficultyFeedback);
            }

            if (_isLastDifficultyChosed)
            {
                if (TryGuessWordLetter(mask))
                {
                    _hasToChangeDifficulty = true;
                    _guessedWordsCount++;
                }
                _attemptsCount--;
                UpdateUI(mask, choiceDifficultyFeedback);
            }

            if (_attemptsCount <= 0)
            {
                _hasToChangeDifficulty = true;
            }
        }
        private string ChooseDifficulty()
        {
            var difficultyNumber = Input.ReadInput();

            if (_difficulty.TrySetDifficulty(difficultyNumber))
            {
                _isLastDifficultyChosed = true;
                _hasToChangeDifficulty = false;

                _attemptsCount = _difficulty.AttemptSettings[_difficulty.CurrentValue!.Value];
            }

            return difficultyNumber;
        }
        private bool TryGuessWordLetter(string mask)
        {
            var letterInput = Input.ReadInput();

            var letter = letterInput[0];
            if (_word.Contains(letter))
            {
                _lettersBank.AddGuessedLetter(letter);
            }
            else
            {
                _lettersBank.AddFailedLetter(letter);
            }

            mask = _word != null ? _word.GetMask(_lettersBank.GuessedLetters) : string.Empty;

            return !mask.Contains('*');
        }
        private void UpdateUI(string mask, string choiceDifficultyFeedback)
        {
            Console.Clear();

            if (_hasToChangeDifficulty)
            {
                UI.PrintChoiceDifficulty();
                return;
            }
            else
            {
                if (_isLastDifficultyChosed)
                    UI.PrintDifficulty(_difficulty.CurrentValue!.Value);
                else if (choiceDifficultyFeedback != string.Empty)
                    UI.PrintSettingDifficultyFailedMessage(choiceDifficultyFeedback);
            }

            UI.PrintGuessedWordsCount(_guessedWordsCount);

            if (_attemptsCount > 0)
                UI.PrintLeftAttempts(_attemptsCount);
            else
                UI.PrintLoseMessage();

            var guessedLetters = _lettersBank.GuessedLetters;
            if (guessedLetters.Any())
            {
                UI.PrintGuessedLetters(guessedLetters);
            }

            var failedLetters = _lettersBank.FailedLetters;
            if (failedLetters.Any())
            {
                UI.PrintFailedLetters(failedLetters);
            }

            if (mask != string.Empty)
                UI.PrintMask(mask);
        }
    }
}
