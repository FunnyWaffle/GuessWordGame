namespace GuessWordGame
{
    public class GameUI
    {
        private readonly List<Text> _playedGamesStatisticsUI = new();

        private readonly Text _startSettingDificultyUIElement = UI.CreateText("Choose diffuculty: \n" +
                     "1 - Easy\n" +
                     "2 - Medium\n" +
                     "3 - Hard");
        private readonly Text _failSettingDificultyUIElement
            = UI.CreateText("Input value {0} is not any Difficult Type. Please, enter new currect value.");
        private readonly Text _difficultyUIElement = UI.CreateText("Difficult is {0}");
        private readonly Text _leftAttemptsUIElement = UI.CreateText("You have {0} attempts.");
        private readonly Text _wordMaskUI = UI.CreateText("Word {0}.");
        private readonly Text _guessedLettersUI = UI.CreateText("Guessed letters: {0}");
        private readonly Text _failedLettersUI = UI.CreateText("Failed letters: {0}");
        private readonly Text _loseMessageUI = UI.CreateText("You lost, because you spend all attempts.");
        private readonly Text _guessedWordsCountUI = UI.CreateText("Guessed words in a raw count: {0}");
        private readonly Text _statisticsTitleUI = UI.CreateText("Statistics:");

        public void HandleDifficultyChange(DifficultyType? difficulty)
        {
            if (difficulty.HasValue)
            {
                _difficultyUIElement.Setparameters(difficulty.Value);
                _difficultyUIElement.IsActive = true;
                _failedLettersUI.IsActive = false;
                _startSettingDificultyUIElement.IsActive = false;
                _failSettingDificultyUIElement.IsActive = false;
                _guessedWordsCountUI.IsActive = false;
                DisablePlayedGamesStatistics();
            }
            else
            {
                _difficultyUIElement.IsActive = false;
                _startSettingDificultyUIElement.IsActive = true;
            }
        }
        public void HandleDifficultyChangeFail(string message)
        {
            _failSettingDificultyUIElement.Setparameters(message);
            _failSettingDificultyUIElement.IsActive = true;
        }
        public void HandleAttemptsCountChange(int count)
        {
            if (count > 0)
            {
                _leftAttemptsUIElement.Setparameters(count);
                _leftAttemptsUIElement.IsActive = true;
                _loseMessageUI.IsActive = false;
            }
            else if (count == 0)
            {
                _leftAttemptsUIElement.IsActive = false;
                _loseMessageUI.IsActive = true;
            }
            else if (count < 0)
            {
                _leftAttemptsUIElement.IsActive = false;
            }
        }
        public void HandleGuessedLettersChage(IEnumerable<char> letters)
        {
            if (letters.Any())
            {
                _guessedLettersUI.Setparameters(string.Concat(letters));
                _guessedLettersUI.IsActive = true;
            }
            else
            {
                _guessedLettersUI.IsActive = false;
            }
        }
        public void HandleFailedLettersChage(IEnumerable<char> letters)
        {
            if (letters.Any())
            {
                _failedLettersUI.Setparameters(string.Concat(letters));
                _failedLettersUI.IsActive = true;
            }
            else
            {
                _failedLettersUI.IsActive = false;
            }
        }
        public void HandleMaskChange(string mask)
        {
            if (string.IsNullOrEmpty(mask))
                _wordMaskUI.IsActive = false;
            else
            {
                _wordMaskUI.IsActive = true;
                _wordMaskUI.Setparameters(mask);
            }
        }

        public void PrintGuessedWordsCount(bool isActive, int value)
        {
            _guessedWordsCountUI.IsActive = isActive;
            _guessedWordsCountUI.Setparameters(value);
        }
        public void PrintPlayedGamesStatistics(IReadOnlyDictionary<DifficultyType, int> playedGamesCountByDifficulty)
        {
            _statisticsTitleUI.IsActive = true;
            foreach (var (difficulty, count) in playedGamesCountByDifficulty)
            {
                if (!TryGetFreeStatisticsUI(out var statisticsUI))
                {
                    statisticsUI = UI.CreateText("You played {0} games on {1} difficulty.");
                }

                statisticsUI.IsActive = true;
                statisticsUI.Setparameters(count, difficulty);

                _playedGamesStatisticsUI.Add(statisticsUI);
            }
        }
        public void DisablePlayedGamesStatistics()
        {
            _statisticsTitleUI.IsActive = false;
            foreach (var statisticUI in _playedGamesStatisticsUI)
            {
                statisticUI.IsActive = false;
            }
        }
        private bool TryGetFreeStatisticsUI(out Text foundFreeStatisticsUI)
        {
            foundFreeStatisticsUI = _playedGamesStatisticsUI.FirstOrDefault(statisticsUI => !statisticsUI.IsActive);
            return foundFreeStatisticsUI != null;
        }
    }
}
