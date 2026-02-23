namespace GuessWordGame
{
    public static class Input
    {
        public static string Value { get; private set; } = string.Empty;
        public static event Action<string> EnterPressed;
        public static void ReadInput()
        {
            EmptyValue();

            if (!Console.KeyAvailable)
                return;

            var keyInfo = Console.ReadKey(true);

            var key = keyInfo.Key;
            if (key == ConsoleKey.Spacebar)
                return;

            if (key == ConsoleKey.Enter)
            {
                if (string.IsNullOrEmpty(Value))
                    return;

                EnterPressed?.Invoke(Value);
                ClearValue();
                return;
            }

            Value += keyInfo.KeyChar;
        }
        private static void ClearValue()
        {
            var valueLenght = Value.Length;
            Value = new string(' ', valueLenght);
        }
        private static void EmptyValue()
        {
            var isValueHasToBeEmpty = true;
            for (int i = 0; i < Value.Length; i++)
            {
                var @char = Value[i];

                if (@char != ' ')
                    isValueHasToBeEmpty = false;
            }
            if (isValueHasToBeEmpty)
                Value = string.Empty;
        }
    }
}
