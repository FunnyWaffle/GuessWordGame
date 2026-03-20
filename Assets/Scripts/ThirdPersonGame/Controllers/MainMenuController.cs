using Assets.Scripts.ThirdPersonGame.View.UI;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class MainMenuController
    {
        private readonly MainMenu _mainMenu;
        private readonly UIMenusSwitcher _uIMenusSwitcher;

        public MainMenuController(MainMenu mainMenu, UIMenusSwitcher uIMenusSwitcher)
        {
            _mainMenu = mainMenu;
            _uIMenusSwitcher = uIMenusSwitcher;

            mainMenu.PlayButtonClicked += HandlePlayButtonCkick;
        }

        public void OpenMenu() => _mainMenu.SetActive(true);

        public void CloseMenu() => _mainMenu.SetActive(false);

        private void HandlePlayButtonCkick() => _uIMenusSwitcher.OpenLevelSelectionMenu();
    }
}
