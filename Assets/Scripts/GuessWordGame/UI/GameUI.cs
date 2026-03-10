using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GuessWordGame.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private StartMenu _startMenu;
        [SerializeField] private GameplayMenu _gameplayMenu;
        [SerializeField] private StatisticsMenu _statisticsMenu;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private Button _mainMenuShowButton;

        private MonoBehaviour _lastClosedWindow;

        public MainMenu MainMenu => _mainMenu;
        public GameplayMenu GameplayMenu => _gameplayMenu;
        public StatisticsMenu StatisticsMenu => _statisticsMenu;

        private void Start()
        {
            _statisticsMenu.SetActive(false);
            _gameplayMenu.SetActive(false);
            _startMenu.SetActive(false);
            ShowMainMenu();
            _gameplayMenu.Initialize();
            _statisticsMenu.Initialize();
            _mainMenu.HideRestartButton();
        }
        private void OnEnable()
        {
            _mainMenuShowButton.onClick.AddListener(ShowMainMenu);

            _mainMenu.PlayButton.ButtonClicked += HandlePlayButtonPress;
            _mainMenu.StatisticsActiveButton.onClick.AddListener(HandleStatisticsButtonClick);
            _mainMenu.RestartButton.onClick.AddListener(HandleGameEnd);

            foreach (var button in _startMenu.UIDifficultySelectionButtons)
            {
                button.ButtonClicked += HanldeDifficultySelectionEvent;
            }
        }
        private void OnDisable()
        {
            _mainMenuShowButton.onClick.RemoveListener(ShowMainMenu);

            _mainMenu.PlayButton.ButtonClicked -= HandlePlayButtonPress;
            _mainMenu.RestartButton.onClick.RemoveListener(HandleGameEnd);

            foreach (var button in _startMenu.UIDifficultySelectionButtons)
            {
                button.ButtonClicked -= HanldeDifficultySelectionEvent;
            }
        }
        public void SubscribeGameOnUIEvents(Game game)
        {
            _startMenu.SubscribeDifficultySelectionEvent(game);
        }
        public void HandleGameStart()
        {
            ShowGameplayMenu();
        }
        public void HandleGameEnd()
        {
            ShowMainMenu();
            MainMenu.PlayButton.ResetState();
            _mainMenu.HideRestartButton();

            GameplayMenu.ClearButtonColors();
            _lastClosedWindow = null;
        }
        private void ShowStartMenu()
        {
            _startMenu.SetActive(true);
            _gameplayMenu.SetActive(false);
            _mainMenu.SetActive(false);
        }
        private void ShowGameplayMenu()
        {
            _startMenu.SetActive(false);
            _gameplayMenu.SetActive(true);
            _mainMenu.SetActive(false);
        }
        private void ShowMainMenu()
        {
            SetLastClosedWindow();
            _statisticsMenu.SetActive(false);
            _gameplayMenu.SetActive(false);
            _startMenu.SetActive(false);
            _mainMenu.SetActive(true);
            _mainMenuShowButton.gameObject.SetActive(false);
        }
        private void HideMainMenu()
        {
            _mainMenu.SetActive(false);
            _mainMenuShowButton.gameObject.SetActive(true);
        }
        private void SetLastClosedWindow()
        {
            if (_startMenu.isActiveAndEnabled)
            {
                _lastClosedWindow = _startMenu;
            }
            else if (_gameplayMenu.isActiveAndEnabled)
            {
                _lastClosedWindow = _gameplayMenu;
            }
        }
        private void HandlePlayButtonPress()
        {
            OpenLastCloseWindow();
            HideMainMenu();
        }
        private void OpenLastCloseWindow()
        {
            if (_lastClosedWindow == null || _lastClosedWindow is StartMenu)
                ShowStartMenu();
            else if (_lastClosedWindow is GameplayMenu)
                _gameplayMenu.SetActive(true);
        }
        private void HandleStatisticsButtonClick()
        {
            HideMainMenu();
            ShowStatisticsMenu();
        }
        private void ShowStatisticsMenu()
        {
            _statisticsMenu.SetActive(true);
        }
        private void HanldeDifficultySelectionEvent(string _)
        {
            _mainMenu.ShowRestartButton();
            _mainMenu.PlayButton.SetSecondState();
        }
    }
}
