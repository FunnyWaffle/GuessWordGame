using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIPlayButton _playButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _statisticsActiveButton;
        [SerializeField] private Button _exitButton;
        public UIPlayButton PlayButton => _playButton;
        public Button StatisticsActiveButton => _statisticsActiveButton;
        public Button RestartButton => _restartButton;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(HandleExit);
        }
        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(HandleExit);
        }
        public void Initialize()
        {
            _restartButton.gameObject.SetActive(false);
        }
        public void ShowRestartButton()
        {
            _restartButton.gameObject.SetActive(true);
            _restartButton.transform.SetSiblingIndex(1);
        }
        public void HideRestartButton()
        {
            _restartButton.gameObject.SetActive(false);
            _restartButton.transform.SetAsLastSibling();
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        private void HandleExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
