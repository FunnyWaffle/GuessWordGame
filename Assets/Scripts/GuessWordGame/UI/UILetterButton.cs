using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class UILetterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        private Image _image;

        private Color _defaultColor = Color.white;
        private Color _mouseOverColor = Color.lightGray;
        private Color _guessedColor = Color.green;
        private Color _failedColor = Color.red;

        private bool _selected = false;
        public bool IsUsed { get; private set; } = false;

        public event Action<char> ButtonClicked;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsUsed)
                return;

            if (!_selected)
                _image.color = _mouseOverColor;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsUsed)
                return;

            if (!_selected)
                _image.color = _defaultColor;
        }
        public void Initialize(GameObject gameObject)
        {
            _button = gameObject.GetComponent<Button>();
            _image = gameObject.GetComponent<Image>();

            _text = gameObject.GetComponentInChildren<TMP_Text>();

            var buttonClickEvent = _button.onClick;
            buttonClickEvent.AddListener(HandleButtonClickEvent);
        }
        public void SetText(char value)
        {
            var stringValue = value.ToString();
            _text.text = stringValue;
            gameObject.name = stringValue;
        }
        public bool IsTextContainsThisLetter(char letter)
        {
            return _text.text[0] == char.ToUpper(letter) ||
                _text.text[0] == char.ToLower(letter);
        }
        public void SetGuessedState(bool isGuessed)
        {
            if (isGuessed)
            {
                _image.color = _guessedColor;
                IsUsed = true;
            }
            else
            {
                _image.color = _failedColor;
                IsUsed = true;
            }
        }
        public void ResetState()
        {
            _selected = false;
            IsUsed = false;
            _image.color = _defaultColor;
        }
        private void HandleButtonClickEvent()
        {
            var text = _text.text;
            if (text.Length != 1)
            {
                Debug.LogError($"Letter button text must be letter {text}");
            }
            ButtonClicked?.Invoke(text[0]);
        }
    }
}
