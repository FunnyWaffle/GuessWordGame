using GuessWordGame;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.GuessWordGame
{
    public static class UI
    {
        private static readonly Dictionary<int, int> _lastFrameSymbolsCountByLines = new();
        private static readonly Dictionary<int, int> _thisFrameSymbolsCountByLines = new();
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
                        _thisFrameSymbolsCountByLines[lines] = line.Length;
                        lines++;
                    }
                }
            }

            ClearRestLines(lines);

            Console.SetCursorPosition(0, lines);

            if (TryGetInput(out var input))
            {
                _thisFrameSymbolsCountByLines[lines] = input.Length;
                Console.WriteLine(input);
            }

            CopyThisFrameSymbolsIntoLastFrame();
        }
        private static void ReplaceLine(int lineNumber, string line)
        {
            if (!_lastFrameSymbolsCountByLines.TryGetValue(lineNumber, out var lastFrameSymbolsCount))
            {
                Console.WriteLine(line);
                return;
            }

            var extraSymbols = lastFrameSymbolsCount - line.Length;
            if (extraSymbols > 0)
                line += new string(' ', extraSymbols);

            Console.WriteLine(line);
        }
        private static void ClearRestLines(int startLineNumber)
        {
            foreach (var (line, symbolsCount) in _lastFrameSymbolsCountByLines)
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
            _lastFrameSymbolsCountByLines.Clear();
            foreach (var (lane, symbolsCount) in _thisFrameSymbolsCountByLines)
            {
                _lastFrameSymbolsCountByLines[lane] = symbolsCount;
            }
            _thisFrameSymbolsCountByLines.Clear();
        }
    }
}
