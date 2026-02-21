namespace GuessWordGame
{
    public static class UI
    {
        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static void PrintChoiceDifficulty()
        {
            Console.WriteLine("Choose diffuculty: \n" +
                    "1 - Easy\n" +
                    "2 - Medium\n" +
                    "3 - Hard");
        }
        public static void PrintDifficulty(DifficultyType value)
        {
            Console.WriteLine($"\nDifficult is {value}");
        }
        public static void PrintGuessedLetters(IEnumerable<char> letters)
        {
            Console.WriteLine("Guessed letters: " + string.Concat(letters));
        }
        public static void PrintFailedLetters(IEnumerable<char> letters)
        {
            Console.WriteLine("Failed letters: " + string.Concat(letters));
        }
        public static void PrintMask(string mask)
        {
            Console.WriteLine($"Word {mask}.");
        }
        public static void PrintSettingDifficultyFailedMessage(string difficultyType)
        {
            Console.WriteLine($"Input value {difficultyType} is not any Difficult Type. Please, enter new currect value.");
        }
        public static void PrintLoseMessage()
        {
            Console.WriteLine("You lost, because you spend all attempts.");
        }
        public static void PrintLeftAttempts(int value)
        {
            Console.WriteLine($"You have {value} attempts.");
        }
        public static void PrintGuessedWordsCount(int value)
        {
            Console.WriteLine($"Guessed words count: {value}");
        }
    }
}
