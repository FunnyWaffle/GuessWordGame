using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ThirdPersonGame.View.UI.EntryPoint
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playbutton;

        public Action PlayButtonClicked;

        private void OnEnable() =>
            _playbutton.onClick.AddListener(HandlePlayButtonClick);

        private void OnDisable() =>
            _playbutton.onClick.RemoveListener(HandlePlayButtonClick);


        private void HandlePlayButtonClick() => PlayButtonClicked?.Invoke();

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}