using Assets.Scripts.ThirdPersonGame.UI;

namespace Assets.Scripts.ThirdPersonGame.Controllers
{
    public class LevelSelectionController : IUIController
    {
        private readonly LevelSelectionMenu _levelSelectionMenu;

        public LevelSelectionController(LevelSelectionMenu levelSelectionMenu)
        {
            _levelSelectionMenu = levelSelectionMenu;
        }

        public void CloseMenu() => _levelSelectionMenu.SetActive(true);

        public void OpenMenu() => _levelSelectionMenu.SetActive(false);
    }
}
