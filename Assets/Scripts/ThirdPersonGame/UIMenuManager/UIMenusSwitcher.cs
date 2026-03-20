using Assets.Scripts.ThirdPersonGame.Controllers;
using Assets.Scripts.ThirdPersonGame.Controllers.LevelSelection;

namespace Assets.Scripts.ThirdPersonGame
{
    public class UIMenusSwitcher
    {
        private MainMenuController _mainMenuController;
        private LevelSelectionMenuController _levelSelectionController;

        public void SetControllers(MainMenuController mainMenuController, LevelSelectionMenuController levelSelectionController)
        {
            _mainMenuController = mainMenuController;
            _levelSelectionController = levelSelectionController;

            OpenMainMenu();
        }

        public void OpenMainMenu()
        {
            _levelSelectionController.CloseMenu();
            _mainMenuController.OpenMenu();
        }

        public void OpenLevelSelectionMenu()
        {
            _mainMenuController.CloseMenu();
            _levelSelectionController.OpenMenu();
        }
    }
}
