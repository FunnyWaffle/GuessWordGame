using Assets.Scripts.GuessWordGame.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class GameplayMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text _mask;
        [SerializeField] private TMP_Text _attemptsCount;
        [SerializeField] private TMP_Text _guessedWordsCount;
        [SerializeField] private TMP_Text _guessedLetters;
        [SerializeField] private TMP_Text _failedLetters;
        [SerializeField] private Transform _lettersGrid;
        [SerializeField] private Image _healthImage;
        [SerializeField] private TMP_Text _healthAmount;
        private int _maxLifeCount;
        public List<UILetterButton> LetterButtons { get; private set; } = new();
        public Task ButtonsInitializeTask { get; private set; }
        public void Initialize()
        {
            ButtonsInitializeTask = InitailizeButtons();
        }
        public void HandleMaskChange(string mask)
        {
            _mask.SetText(mask);
        }
        public void HandleAttemptsCountChange(int value)
        {
            _attemptsCount.SetText(value.ToString());
        }
        public void HandleGuessedWordsCountChange(int value)
        {
            _guessedWordsCount.SetText(value.ToString());
        }
        public void HandleGuessedLettersChange(IEnumerable<char> letters)
        {
            _guessedLetters.SetText(letters.ToArray());
            SetLetterButtonStates(letters, true);
        }
        public void HandleFailedLettersChange(IEnumerable<char> letters)
        {
            _failedLetters.SetText(letters.ToArray());
            SetLetterButtonStates(letters, false);
        }
        public void HandleMaxLifeCountChange(int value)
        {
            _maxLifeCount = value;
        }
        public void HandleCurrentLifeCountChange(int value)
        {
            _healthImage.fillAmount = (float)value / _maxLifeCount;
            _healthAmount.text = value.ToString();
        }
        public void ClearButtonColors()
        {
            foreach (var button in LetterButtons)
            {
                button.ResetState();
            }
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        private async Task InitailizeButtons()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (var letter in letters)
            {
                var prefab = await AssetsLoader.LoadAsync("LetterButton");

                var gameObject = GameObject.Instantiate(prefab, _lettersGrid);

                var button = gameObject.AddComponent<UILetterButton>();
                button.Initialize(gameObject);

                button.SetText(letter);

                LetterButtons.Add(button);
            }
        }
        private void SetLetterButtonStates(IEnumerable<char> letters, bool isGuessed)
        {
            foreach (var letter in letters)
            {
                foreach (var button in LetterButtons)
                {
                    if (!button.IsUsed &&
                        button.IsTextContainsThisLetter(letter))
                    {
                        button.SetGuessedState(isGuessed);
                    }
                }
            }
        }
    }
}
