namespace GuessWordGame
{
    public class Word
    {
        private readonly string _value;
        public Word(string value)
        {
            _value = value;
        }
        public bool Contains(char letter)
        {
            return _value.Contains(letter, StringComparison.OrdinalIgnoreCase);
        }
        public string GetMask(IEnumerable<char> usedLetters)
        {
            var mask = string.Empty;

            foreach (char letter in _value)
            {
                if (usedLetters.Contains(char.ToUpper(letter))
                    || usedLetters.Contains(char.ToLower(letter)))
                    mask += letter;
                else
                    mask += "*";
            }

            return mask;
        }
    }
}
