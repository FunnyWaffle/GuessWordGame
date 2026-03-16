using Assets.Scripts.ThirdPersonGame.Controllers;
using Assets.Scripts.ThirdPersonGame.UI;

namespace Assets.Scripts.ThirdPersonGame
{
    public static class UIMenuManager
    {
        private static MainMenuController _mainMenuController;
        private static LevelSelectionController _levelSelectionController;

        public static void CreateMainMenuController(MainMenu mainMenu) => _mainMenuController = new MainMenuController(mainMenu);

        public static void CreateLevelSelectionMenuController(LevelSelectionMenu levelSelectionMenu) =>
            _levelSelectionController = new LevelSelectionController(levelSelectionMenu);

        public static void OpenMainMenu()
        {
            _levelSelectionController.CloseMenu();
            _mainMenuController.OpenMenu();
        }

        public static void OpenLevelSelectionMenu()
        {
            _mainMenuController.CloseMenu();
            _levelSelectionController.OpenMenu();
        }
    }
}
