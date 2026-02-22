namespace GuessWordGame
{
    public static class UI
    {
        private static readonly Dictionary<int, int> _lastFrameSymbolsCountByLanes = new();
        private static readonly Dictionary<int, int> _thisFrameSymbolsCountByLanes = new();
        private readonly static List<UIElement> _uIElements = new();

        public static void HideCursor()
        {
            Console.CursorVisible = false;
        }
        public static Text CreateText(string text)
        {
            var uIText = new Text(text);
            _uIElements.Add(uIText);
            return uIText;
        }
        public static void UpdateUI()
        {
            Console.SetCursorPosition(0, 0);

            var lines = 0;
            foreach (var element in _uIElements)
            {
                if (!element.IsActive)
                    continue;

                if (element is Text text)
                {
                    var splitLines = text.Value.Split('\n');
                    foreach (var line in splitLines)
                    {
                        ReplaceLine(lines, line);
                        _thisFrameSymbolsCountByLanes[lines] = line.Length;
                        lines++;
                    }
                }
            }

            ClearRestLines(lines);

            Console.SetCursorPosition(0, lines);

            if (TryGetInput(out var input))
            {
                _thisFrameSymbolsCountByLanes[lines] = input.Length;
                Console.WriteLine(input);
            }

            CopyThisFrameSymbolsIntoLastFrame();
        }
        private static void ReplaceLine(int lineNumber, string line)
        {
            if (!_lastFrameSymbolsCountByLanes.TryGetValue(lineNumber, out var lastFrameSymbolsCount))
            {
                Console.WriteLine(line);
                return;
            }

            for (int i = 0; i < lastFrameSymbolsCount - line.Length; i++)
            {
                line += ' ';
            }

            Console.WriteLine(line);
        }
        private static void ClearRestLines(int startLineNumber)
        {
            foreach (var (line, symbolsCount) in _lastFrameSymbolsCountByLanes)
            {
                if (line < startLineNumber)
                    continue;

                Console.SetCursorPosition(0, line);
                Console.WriteLine(new string(' ', symbolsCount));
            }
        }
        private static bool TryGetInput(out string input)
        {
            input = Input.Value;
            if (input == string.Empty)
                return false;
            return true;
        }
        private static void CopyThisFrameSymbolsIntoLastFrame()
        {
            _lastFrameSymbolsCountByLanes.Clear();
            foreach (var (lane, symbolsCount) in _thisFrameSymbolsCountByLanes)
            {
                _lastFrameSymbolsCountByLanes[lane] = symbolsCount;
            }
            _thisFrameSymbolsCountByLanes.Clear();
        }

        
    }
}
