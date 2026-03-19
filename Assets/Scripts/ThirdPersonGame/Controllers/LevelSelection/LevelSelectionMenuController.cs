using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View.UI.LevelSelection;
using System.Collections.Generic;

namespace Assets.Scripts.ThirdPersonGame.Controllers.LevelSelection
{
    public class LevelSelectionMenuController
    {
        private readonly LevelSelectionMenu _levelSelectionMenu;
        private readonly List<LevelSelectionButtonController> _levelSelectionButtonControllers = new();

        public LevelSelectionMenuController(Game game, LevelSelectionMenu levelSelectionMenu)
        {
            _levelSelectionMenu = levelSelectionMenu;

            foreach (var button in game.LevelSelectionButtons)
            {
                _levelSelectionButtonControllers.Add(new LevelSelectionButtonController(button,
                    levelSelectionMenu.CreateLevelSelectionButtonView()));
            }
        }

        public void CloseMenu() => _levelSelectionMenu.SetActive(false);

        public void OpenMenu() => _levelSelectionMenu.SetActive(true);

    }
}
