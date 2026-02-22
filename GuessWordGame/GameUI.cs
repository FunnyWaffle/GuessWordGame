namespace GuessWordGame
{
    public class GameUI
    {
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
        private readonly Text _guessedWordsCountUI = UI.CreateText("Guessed words count: {0}");
        public void PrintLoseMessage(bool isActive)
        {
            _loseMessageUI.IsActive = isActive;
        }
        public void PrintGuessedWordsCount(bool isActive, int value)
        {
            _guessedWordsCountUI.IsActive = isActive;
            _guessedWordsCountUI.Setparameters(value);
        }
        public void PrintGuessedLetters(bool isActive, string letters)
        {
            _guessedLettersUI.IsActive = isActive;
            _guessedLettersUI.Setparameters(letters);
        }
        public void PrintFailedLetters(bool isActive, string letters)
        {
            _failedLettersUI.IsActive = isActive;
            _failedLettersUI.Setparameters(letters);
        }
        public void PrintMask(bool isActive, string mask)
        {
            _wordMaskUI.IsActive = isActive;
            _wordMaskUI.Setparameters(mask);
        }
        public void SetLeftAttemptsUIElementState(bool isActive, int value)
        {
            _leftAttemptsUIElement.IsActive = isActive;
            _leftAttemptsUIElement.Setparameters(value);
        }
        public void SetStartSettingDificultyUIElementActive(bool isActive)
        {
            _startSettingDificultyUIElement.IsActive = isActive;
        }
        public void ShowFailSettingDificultyUIElement(bool isActive, string arg = null)
        {
            if (arg != null)
                _failSettingDificultyUIElement.Setparameters(arg);

            _failSettingDificultyUIElement.IsActive = isActive;
        }
        public void PrintDifficulty(bool isActive, string value = null)
        {
            if (value != null)
                _difficultyUIElement.Setparameters(value);

            _difficultyUIElement.IsActive = isActive;
        }
    }
}
