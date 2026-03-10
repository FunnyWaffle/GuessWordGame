using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GuessWordGame.UI
{
    [RequireComponent(typeof(Button))]
    public class UIPlayButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        private readonly string _defaultText = "Play";

        public event Action ButtonClicked;

        private void Start()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();

            ResetState();
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

        public void ResetState()
        {
            _text.text = _defaultText;
        }

        public void SetSecondState()
        {
            _text.text = "Continue";
        }

        private void HandleButtonClickEvent()
        {
            ButtonClicked?.Invoke();
        }

    }
}
