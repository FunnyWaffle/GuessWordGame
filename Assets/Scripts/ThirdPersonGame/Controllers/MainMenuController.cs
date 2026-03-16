using Assets.Scripts.ThirdPersonGame.UI;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class MainMenuController : IUIController
    {
        private readonly MainMenu _mainMenu;
        public MainMenuController(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;

            mainMenu.PlayButtonClicked += HandlePlayButtonCkick;

            UIMenuManager.OpenMainMenu();
        }

        public void OpenMenu() => _mainMenu.SetActive(true);

        public void CloseMenu() => _mainMenu.SetActive(false);

        private void HandlePlayButtonCkick() => UIMenuManager.OpenLevelSelectionMenu();
    }
}
