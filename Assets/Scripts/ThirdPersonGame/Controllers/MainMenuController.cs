using Assets.Scripts.ThirdPersonGame.View.UI;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class MainMenuController
    {
        private readonly MainMenu _mainMenu;

        public MainMenuController(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;

            //mainMenu.PlayButtonClicked += HandlePlayButtonCkick;
        }

        public void OpenMenu() => _mainMenu.SetActive(true);

        public void CloseMenu() => _mainMenu.SetActive(false);

        //private void HandlePlayButtonCkick() => UIMenusSwitcher.OpenLevelSelectionMenu();
    }
}
