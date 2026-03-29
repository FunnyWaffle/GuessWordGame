using Assets.Scripts.ThirdPersonGame.Core;
using Assets.Scripts.ThirdPersonGame.View.UI.EntryPoint.LevelSelection;

namespace Assets.Scripts.ThirdPersonGame.Controllers.LevelSelection
{
    public class LevelSelectionButtonController
    {
        private readonly LevelSelectionButton _levelSelectionButton;
        private readonly LevelSelectionButtonView _levelSelectionButtonView;

        public LevelSelectionButtonController(LevelSelectionButton levelSelectionButton,
            LevelSelectionButtonView levelSelectionButtonView)
        {
            levelSelectionButtonView.SetText(levelSelectionButton.LevelName + levelSelectionButton.LevelId);

            levelSelectionButtonView.ButtonClickedEvent.AddListener(levelSelectionButton.HandleLevelSelection);

            _levelSelectionButton = levelSelectionButton;
            _levelSelectionButtonView = levelSelectionButtonView;
        }
    }
}
