using Assets.Scripts.ThirdPersonGame.Controllers.LevelSelection;
using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View.UI;
using Assets.Scripts.ThirdPersonGame.View.UI.LevelSelection;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class ControllersBuilder
    {
        public void CreateControllers(Game game, UIRoot uIRoot, UIMenusSwitcher uIMenusSwitcher)
        {
            var levelSelectionMenuController = CreateLevelSelectionMenuController(game, uIRoot.LevelSelectionMenu);
            var mainMenuController = CreateMainMenuController(uIRoot.MainMenu, uIMenusSwitcher);

            uIMenusSwitcher.SetControllers(mainMenuController, levelSelectionMenuController);
        }

        private LevelSelectionMenuController CreateLevelSelectionMenuController(Game game, LevelSelectionMenu levelSelectionMenu) =>
           new(game, levelSelectionMenu);

        private MainMenuController CreateMainMenuController(MainMenu mainMenu, UIMenusSwitcher uIMenusSwitcher) =>
           new(mainMenu, uIMenusSwitcher);
    }
}
