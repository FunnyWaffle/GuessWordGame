using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GuessWordGame.UI
{
    [RequireComponent(typeof(Button))]
    public class UIDifficultySelectionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public event Action<string> ButtonClicked;

        private void Start()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            var buttonClickEvent = _button.onClick;
            buttonClickEvent.AddListener(HandleButtonClickEvent);
        }

        private void OnDisable()
        {
            var buttonClickEvent = _button.onClick;
            buttonClickEvent.RemoveListener(HandleButtonClickEvent);
        }

        public void SetText(string value)
        {
            _text.text = value;
        }

        private void HandleButtonClickEvent()
        {
            ButtonClicked?.Invoke(_text.text);
        }
    }
}
