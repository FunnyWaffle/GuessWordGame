using System;
using System.Linq;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GuessWordGame
{
    public class Game
    {
        private readonly WordGenerator _wordGenerator;

        private Difficulty? _difficulty;
        private Word _word;

        private int _attemptsCount;
        private int _guessedWordsCount;
        private int _maxLifeCount;
        private int _currentLifeCount;

        public Game()
        {
            _wordGenerator = new WordGenerator();
            LettersBank = new LettersBank();
            Statistics = new Statistics();

            Keyboard.current.onTextInput += HandleInput;
        }

        public LettersBank LettersBank { get; }
        public Statistics Statistics { get; }
        private int MaxLifeCount
        {
            get => _maxLifeCount;
            set
            {
                _maxLifeCount = value;
                MaxLifeCountChanged?.Invoke(_maxLifeCount);
            }
        }
        private int CurrentLifeCount
        {
            get => _currentLifeCount;
            set
            {
                _currentLifeCount = value;
                CurrentLifeCountChanged?.Invoke(_currentLifeCount);
            }
        }
        private int AttemptsCount
        {
            get => _attemptsCount;
            set
            {
                _attemptsCount = value;
                AttemptsCountChanged?.Invoke(_attemptsCount);
            }
        }
        private int GuessedWordsCount
        {
            get => _guessedWordsCount;
            set
            {
                _guessedWordsCount = value;
                GuessedWordsCountChanged?.Invoke(_guessedWordsCount);
            }
        }


        public event Action GameStarted;
        public event Action<string> WordMaskChanged;
        public event Action<int> AttemptsCountChanged;
        public event Action<int> GuessedWordsCountChanged;
        public event Action<int> MaxLifeCountChanged;
        public event Action<int> CurrentLifeCountChanged;
        public event Action WordGenerated;
        public event Action GameEnded;

        public void HandleInput(char input)
        {
            if (!_difficulty.HasValue)
                return;

            if (!char.IsLetter(input))
                return;

            var mask = GuessWordLetter(input);

            var isWin = !mask.Contains('*');
            var isLost = AttemptsCount <= 0;
            var isGameOver = isWin || isLost;

            if (!isGameOver)
                return;

            Statistics.SetPlayedGame(_difficulty.Value, isWin);

            var config = Config.DifficultyConfigs[_difficulty.Value];
            GenerateNextWord(config);

            if (isWin)
            {
                GuessedWordsCount++;
            }
            else
            {
                CurrentLifeCount--;
            }

            if (CurrentLifeCount <= 0)
            {
                GameEnded?.Invoke();
            }
        }

        public void ChooseDifficulty(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            if (!DifficultyParser.TryParse(input, out var difficulty))
                return;

            _difficulty = difficulty;

            var config = Config.DifficultyConfigs[_difficulty.Value];
            GenerateNextWord(config);

            MaxLifeCount = config.LifeCount;
            CurrentLifeCount = config.LifeCount;

            GameStarted?.Invoke();
        }

        public void RestartGame()
        {
            Reset();
            GameEnded?.Invoke();
        }

        private string GuessWordLetter(char letter)
        {
            if (_word.Contains(letter))
            {
                LettersBank.AddGuessedLetter(letter);
            }
            else if (!LettersBank.FailedLetters.Contains(letter))
            {
                LettersBank.AddFailedLetter(letter);
                AttemptsCount--;
            }

            var mask = _word.GetMask(LettersBank.GuessedLetters);
            WordMaskChanged?.Invoke(mask);
            return mask;
        }

        private void GenerateNextWord(DifficultyConfig config)
        {
            LettersBank.Reset();

            while (!_wordGenerator.TryGenerateWordByLenght(config.MinWordLenght, config.MaxWordLenght, out _word))
            {
            }

            AttemptsCount = config.GuessLetterAttemtsCount;

            WordGenerated?.Invoke();
            WordMaskChanged?.Invoke(_word.GetMask(LettersBank.GuessedLetters));
        }

        private void Reset()
        {
            _word = null;
            _difficulty = null;
            GuessedWordsCount = 0;
        }
    }
}
