namespace GuessWordGame
{
    public static class Input
    {
        public static string ReadInput()
        {
            var input = Console.ReadLine();
            if (input == null || input == string.Empty)
                return ReadInput();
            else
                return input;
        }
    }
}
