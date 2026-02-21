namespace GuessWordGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var difficulty = new Difficulty();
            ResetDifficulty(difficulty);
            var wordBank = new WordBank(difficulty.WordSettings);

            var guessedWordsCount = 0;
            while (true)
            {
                wordBank.TryGenerateWordByDifficulty(difficulty.CurrentValue!.Value, out var word);
                var isWordGuessed = GuessWordLetters(word, difficulty);
                ResetDifficulty(difficulty);

                if (isWordGuessed)
                    UI.PrintGuessedWordsCount(++guessedWordsCount);
            }
        }
        private static void ResetDifficulty(Difficulty difficulty)
        {
            var wasDifficultySetted = false;
            while (!wasDifficultySetted)
            {
                UI.PrintChoiceDifficulty();

                var difficultyNumber = Input.ReadInput();
                Console.Clear();

                wasDifficultySetted = difficulty.TrySetDifficulty(difficultyNumber);

                if (!wasDifficultySetted)
                    UI.PrintSettingDifficultyFailedMessage(difficultyNumber);
            }
            UI.PrintDifficulty(difficulty.CurrentValue!.Value);
        }
        private static bool GuessWordLetters(Word word, Difficulty difficulty)
        {
            var guessedLetters = new HashSet<char>();
            var failedLetters = new HashSet<char>();

            var maxAttempts = difficulty.AttemptSettings[difficulty.CurrentValue!.Value];
            var currentAttempts = 0;

            var mask = word.GetMask(guessedLetters);
            bool isWordNotGuessed = true;
            while (isWordNotGuessed)
            {
                UI.PrintGuessedLetters(guessedLetters);
                UI.PrintFailedLetters(failedLetters);
                UI.PrintLeftAttempts(maxAttempts - currentAttempts);

                if (currentAttempts >= maxAttempts)
                {
                    UI.PrintLoseMessage();
                    break;
                }

                var letterInput = Input.ReadInput();
                currentAttempts++;

                var letter = letterInput[0];
                if (word.Contains(letter))
                {
                    guessedLetters.Add(letter);
                    mask = word.GetMask(guessedLetters);
                }
                else
                {
                    failedLetters.Add(letter);
                }

                Console.Clear();
                UI.PrintDifficulty(difficulty.CurrentValue!.Value);
                UI.PrintMask(mask);

                isWordNotGuessed = mask.Contains('*');
            }

            return !isWordNotGuessed;
        }
    }
}
